using System;
using Payments.Iso4217Currencies;

namespace Payments
{
	public static partial class Iso4217
	{
		public static bool Contain(ICurrency currency)
		{
			return currency is Currency;
		}
		
		public static bool Contain(string charCode)
		{
			return Parse(charCode) != null;
		}
		
		public static bool TryParse(string charCode, out ICurrency currency)
		{
			currency = Parse(charCode);
			if(currency != null)
				return true;
			currency = new UnknownCurrency(charCode);
			return false;
		}
		
		public static bool TryParse(int numCode, out ICurrency currency)
		{
			currency = Parse(numCode);
			if(currency != null)
				return true;
			currency = new UnknownCurrency(numCode);
			return false;
		}
	}
}