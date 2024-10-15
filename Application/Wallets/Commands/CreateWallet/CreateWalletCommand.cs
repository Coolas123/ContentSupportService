using Application.Abstractions.Messaging;
using Domain.Enums;

namespace Application.Wallets.Commands.CreateWallet
{
    public class CreateWalletCommand : ICommand
    {
        public Guid UserId { get; init; }
        public Country Country { get; init; }

        public CreateWalletCommand(Guid userId, Country country)
        {
            UserId = userId;
            Country = country;
        }
    }
}
