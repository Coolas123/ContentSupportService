using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Wallets.Commands.CreateWallet
{
    internal class CreateWalletCommandHandler : ICommandHandler<CreateWalletCommand>
    {
        private readonly IWalletRepository walletRepository;

        public CreateWalletCommandHandler(IWalletRepository walletRepository)
        {
            this.walletRepository = walletRepository;
        }
        public async Task<Result> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = new Wallet(
                userId: request.UserId,
                currency: Currency.GetFromValue((int)request.Country)
                );

            await walletRepository.CreateAsync(wallet);

            return Result.Success();
        }
    }
}
