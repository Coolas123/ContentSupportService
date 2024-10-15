using Application.Abstractions.Messaging;
using Application.HelpClasses;
using Domain.Entities;
using Domain.EntityServices;
using Domain.Repositories;
using Domain.Shared;
using System.Security.Claims;

namespace Application.Users.Commands.RegisterUser
{
    public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, ClaimsIdentity>
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailUniqueCheck emailUniqueCheck;
        private readonly IUnitOfWork unitOfWork;

        public RegisterUserCommandHandler(IUserRepository userRepository, IEmailUniqueCheck emailUniqueCheck,
            IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.emailUniqueCheck = emailUniqueCheck;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<ClaimsIdentity>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userResult = await User.CreateAsync(
                Guid.NewGuid(),
                request.UserName,
                request.DateOfBirth,
                request.Email,
                request.Gender,
                HashPassword.Generate(request.Password),
                Domain.Enums.SystemRole.User,
                Domain.Enums.UserRole.Patron,
                request.Country,
                emailUniqueCheck);

            if (userResult.IsSuccess)
            {
                await userRepository.CreateAsync(userResult.Value());

                var calimIndentity = userResult.Value().Authenticate();

                await unitOfWork.SaveChangesAsync();

                return Result.Success(calimIndentity.Value());
            }

            return Result.Failure<ClaimsIdentity>(Error.None);
        }
    }
}
