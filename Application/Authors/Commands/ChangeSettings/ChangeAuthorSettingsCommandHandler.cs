using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.EntityServices.UserServices;
using Domain.Enums;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.AspNetCore.Hosting;

namespace Application.Authors.Commands.ChangeSettings
{
    public sealed class ChangeAuthorSettingsCommandHandler : ICommandHandler<ChangeAuthorSettingsCommand,bool>
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ISaveProfileMaterial saveProfileMaterial;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IProfileMaterialRepository profileMaterialRepository;

        public ChangeAuthorSettingsCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork,
            ISaveProfileMaterial saveProfileMaterial,
            IWebHostEnvironment webHostEnvironment, IProfileMaterialRepository profileMaterialRepository) {
            this.authorRepository = authorRepository;
            this.unitOfWork = unitOfWork;
            this.saveProfileMaterial = saveProfileMaterial;
            this.webHostEnvironment = webHostEnvironment;
            this.profileMaterialRepository = profileMaterialRepository;
        }
        public async Task<Result<bool>> Handle(ChangeAuthorSettingsCommand request, CancellationToken cancellationToken) {
            var author = await authorRepository.GetByIdWithUserAndProfileMaterialsAsync(request.UserId);

            author.ChangeSettings(request.Description);

            if(request.BannerFile != null) {
                string path = "/Files/Account/Banners/" + author?.Id + request.BannerFile.FileName;
                
                var profileMaterial = author?.User?.ProfileMaterials?.FirstOrDefault(x => x.MaterialType == MaterialType.Banner);

                if (profileMaterial != null) {
                    profileMaterial.Path = path;

                    profileMaterial.Title = request.BannerFile.FileName;

                    author?.User?.ChangeProfileMaterial(profileMaterial, MaterialType.Banner);
                }
                else {
                    var newProfileMaterial = ProfileMaterial.Create(Guid.NewGuid(), author.Id, MaterialType.Banner, request.BannerFile.FileName, path);

                    author?.User?.ChangeProfileMaterial(newProfileMaterial.Value(), MaterialType.Banner);

                    await profileMaterialRepository.CreateAsync(newProfileMaterial.Value());
                }

                await saveProfileMaterial.Save(request.BannerFile, author.Id, path, webHostEnvironment);
            }

            authorRepository.Update(author!);

            await unitOfWork.SaveChangesAsync();

            return Result.Success(false);
        }
    }
}
