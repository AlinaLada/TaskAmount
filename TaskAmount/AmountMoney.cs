using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TaskAmount
{
    public class AmountMoney
    {
        public decimal SizeOfAmount { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }

        public AmountMoney(decimal sizeOfAmount, string currencyCode, decimal exchangeRate)
        {
            SizeOfAmount = sizeOfAmount;
            CurrencyCode = currencyCode;
            ExchangeRate = exchangeRate;
        }

        public int Compare(AmountMoney other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            decimal thisInUSD = ConvertToUSD();
            decimal otherInUSD = other.ConvertToUSD();

            if (thisInUSD < otherInUSD) return -1;
            if (thisInUSD > otherInUSD) return 1;
            return 0;
        }

        public AmountMoney Add(AmountMoney other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            decimal thisInUSD = ConvertToUSD();
            decimal otherInUSD = other.ConvertToUSD();
            decimal totalInUSD = thisInUSD + otherInUSD;

            return new AmountMoney(totalInUSD / ExchangeRate, CurrencyCode, ExchangeRate);
        }

        public decimal ConvertToUSD()
        {
            if (ExchangeRate <= 0) throw new InvalidOperationException("Exchange rate must be greater than zero.");
            return SizeOfAmount * ExchangeRate;
        }

        public override bool Equals(object obj)
        {
            if (obj is AmountMoney other)
            {
                return SizeOfAmount == other.SizeOfAmount &&
                       CurrencyCode == other.CurrencyCode &&
                       ExchangeRate == other.ExchangeRate;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{SizeOfAmount} {CurrencyCode} (Rate: {ExchangeRate})";
        }
    }
}
