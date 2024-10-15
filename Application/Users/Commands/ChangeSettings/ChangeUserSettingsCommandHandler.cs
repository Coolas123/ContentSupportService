using Application.Abstractions.Messaging;
using Application.HelpClasses;
using Domain.Entities;
using Domain.EntityServices.UserServices;
using Domain.Enums;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.AspNetCore.Hosting;

namespace Application.Users.Commands.ChangeSettings
{
    public sealed class ChangeUserSettingsCommandHandler : ICommandHandler<ChangeUserSettingsCommand, bool>
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ISaveProfileMaterial saveProfileMaterial;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProfileMaterialRepository profileMaterialRepository;

        public ChangeUserSettingsCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork,
            ISaveProfileMaterial saveProfileMaterial,
            IWebHostEnvironment webHostEnvironment, IProfileMaterialRepository profileMaterialRepository) {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.saveProfileMaterial = saveProfileMaterial;
            this.webHostEnvironment = webHostEnvironment;
            this.profileMaterialRepository = profileMaterialRepository;
        }
        public async Task<Result<bool>> Handle(ChangeUserSettingsCommand request, CancellationToken cancellationToken) {
            bool isClaimsChanged = false;

            var user = await userRepository.GetByIdAsync(request.UserId);

            if (user != null) {
                isClaimsChanged = user.ChangeSettings(
                    request.UserName,
                    request.DateOfBirth,
                    request.Email,
                    request.Country,
                    request.Password != null ? HashPassword.Generate(request.Password) : null
                    );
            }

            if(request.AvatarFile != null) {
                string path = "/Files/Account/Avatars/" + user?.Id + request.AvatarFile.FileName;
                
                var profileMaterial = user?.ProfileMaterials?.FirstOrDefault(x => x.MaterialType == MaterialType.Avatar);

                if (profileMaterial != null) {
                    profileMaterial.Path = path;

                    profileMaterial.Title = request.AvatarFile.FileName;

                    user?.ChangeProfileMaterial(profileMaterial, MaterialType.Avatar);
                }
                else {
                    var newProfileMaterial = ProfileMaterial.Create(Guid.NewGuid(),user.Id, MaterialType.Avatar, request.AvatarFile.FileName, path);

                    user.ChangeProfileMaterial(newProfileMaterial.Value(), MaterialType.Avatar);

                    await profileMaterialRepository.CreateAsync(newProfileMaterial.Value());
                }
                await saveProfileMaterial.Save(request.AvatarFile, user.Id, path, webHostEnvironment);
            }

            userRepository.Update(user!);

            await unitOfWork.SaveChangesAsync();

            if (isClaimsChanged) {
                return Result.Success(isClaimsChanged);
            }

            return Result.Success(false);
        }
    }
}
