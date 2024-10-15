using Application.Abstractions.Messaging;
using Application.Users.Commands.RegisterUser;
using Application.Wallets.Commands.CreateWallet;
using Domain.Shared;
using System.Security.Claims;

namespace Application.Users.Commands.RegisterPatronAndWallet
{
    public class RegisterPatronAndWalletCommand : ICommand<ClaimsIdentity>
    {
        public RegisterUserCommand RegisterUserCommand { get; set; }
        public CreateWalletCommand? CreateWalletCommand { get; set; }
    }
}
