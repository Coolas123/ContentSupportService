using Domain.Enums.Currencies;
using Domain.Primitives;
using System;

namespace Domain.Enums
{
    public abstract class Currency : Enumiration<Currency>
    {
        public static readonly Currency RUB = new RUBCurrency();
        public static readonly Currency USD = new USDCurrency();
        protected Currency(int value, string name) : base(value, name) {
        }
        public abstract string Symbol { get; }
    }
}
