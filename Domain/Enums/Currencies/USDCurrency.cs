using Domain.Shared;

namespace Domain.Enums.Currencies
{
    public sealed class USDCurrency : Currency
    {
        public USDCurrency() : base((int)Country.USA, "USD") {
        }

        public override string Symbol => "$";
    }
}
