using Domain.Shared;

namespace Domain.Enums.Currencies
{
    public sealed class RUBCurrency : Currency
    {
        public RUBCurrency() : base((int)Country.Russia, "RUB") {
        }

        public override string Symbol => "₽";
    }
}
