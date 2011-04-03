using System;
using Payments.Iso4217Currencies;

namespace Payments
{
	public static partial class Iso4217
	{
		public static readonly ICurrency UAH = new Currency("UAH", "€", 980, 100m);
		public static readonly ICurrency UGX = new Currency("UGX", "€", 800, 100m);
		public static readonly ICurrency USD = new Currency("USD", "€", 840, 100m);

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
		
		public static ICurrency Parse(string charCode)
		{
			switch(charCode.ToUpperInvariant())
			{
				case "UAH": return UAH;
				case "UGX": return UGX;
				case "USD": return USD;
				default: return null;
			}
		}
	}
}