using Application.Abstractions.Messaging;
using Application.Users.Queries.GetUser;
using Application.Wallets.Commands.CreateWallet;
using Domain.Errors;
using Domain.Shared;
using MediatR;
using System.Security.Claims;

namespace Application.Users.Commands.RegisterPatronAndWallet
{
    public class RegisterPatronAndWalletCommandHandler : ICommandHandler<RegisterPatronAndWalletCommand, ClaimsIdentity>
    {
        private readonly ISender sender;

        public RegisterPatronAndWalletCommandHandler(ISender sender)
        {
            this.sender = sender;
        }
        public async Task<Result<ClaimsIdentity>> Handle(RegisterPatronAndWalletCommand request, CancellationToken cancellationToken)
        {
            var userClaimIdentity = await sender.Send(request.RegisterUserCommand);

            if (userClaimIdentity.IsSuccess) {
                var user = await sender.Send(
                    new GetUserQuery(Guid.Parse(userClaimIdentity.Value().FindFirst("Id").Value)));

                if (user.IsSuccess) {
                    request.CreateWalletCommand =
                    new CreateWalletCommand(user.Value().Id, user.Value().Country);

                    await sender.Send(request.CreateWalletCommand);

                    return userClaimIdentity;
                }
            }
            else {
                return userClaimIdentity;
            }

            return Result.Failure<ClaimsIdentity>(PersistenceError.User.UserCouldNotRegister);
        }
    }
}
