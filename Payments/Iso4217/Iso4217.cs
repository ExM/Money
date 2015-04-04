using System;

namespace AbbyyLS.Payments
{
	/// <summary>
	/// The currencies standard ISO 4217
	/// </summary>
	public static partial class Iso4217
	{
		/// <summary>
		/// ISO 4217 contain this instance of currency
		/// </summary>
		/// <param name="currency">
		/// instance
		/// </param>
		public static bool Contain(ICurrency currency)
		{
			return currency is Iso4217Currency;
		}
		
		/// <summary>
		/// ISO 4217 contains an instance of a character code
		/// </summary>
		/// <param name="charCode">
		/// character code
		/// </param>
		public static bool Contain(string charCode)
		{
			return Parse(charCode) != null;
		}
		
		/// <summary>
		/// Return currency from character code
		/// </summary>
		/// <param name="charCode">
		/// character code
		/// </param>
		/// <param name="currency">
		/// currency
		/// </param>
		/// <returns>
		/// true if ISO 4217 contain this currency
		/// </returns>
		public static bool TryParse(string charCode, out ICurrency currency)
		{
			currency = Parse(charCode);
			if(currency != null)
				return true;
			currency = new UnknownCurrency(charCode);
			return false;
		}
		
		/// <summary>
		/// Return currency from number code
		/// </summary>
		/// <param name="numCode">
		/// number code
		/// </param>
		/// <param name="currency">
		/// currency
		/// </param>
		/// <returns>
		/// true if ISO 4217 contain this currency
		/// </returns>
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