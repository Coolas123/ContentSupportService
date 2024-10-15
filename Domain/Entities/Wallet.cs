using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Wallet : Entity
    {
        public Guid UserId { get; }
        public Currency Currency { get; }

        public decimal Balance { get; private set; }

        public bool IsActive { get; }

        public Wallet(Guid userId, Currency currency) 
            : base(userId) {

            UserId = userId;
            Currency = currency;
            Balance = 0;
            IsActive = true;
        }

        public void ReplenishWallet(decimal sum) {
            Balance += sum;
        }

        public void WithdrawWallet(decimal sum) {
            Balance -= sum;
        }
    }
}
