using System;
namespace Payments
{
	public static class Iso4217
	{
		public static readonly Currency UAH = new Currency("UAH", "€", 980, 100m);
		public static readonly Currency UGX = new Currency("UGX", "€", 800, 100m);
		public static readonly Currency USD = new Currency("USD", "€", 840, 100m);

		public static bool Contain(Currency currency)
		{
			return Parse(currency.CharCode) != null;
		}
		
		public static bool Contain(string charCode)
		{
			return Parse(charCode) != null;
		}
		
		public static bool TryParse(string charCode, out Currency currency)
		{
			currency = Parse(charCode);
			if(currency != null)
				return true;
			currency = new Currency(charCode, "?", -1, 1m);
			return false;
		}
		
		public static Currency Parse(string charCode)
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

