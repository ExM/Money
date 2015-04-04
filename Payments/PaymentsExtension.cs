using System;

namespace AbbyyLS.Payments
{
	/// <summary>
	/// any extensions
	/// </summary>
	public static class PaymentsExtension
	{
		/// <summary>
		/// Create money from this currency
		/// </summary>
		/// <param name="currency">
		/// currency
		/// </param>
		/// <param name="amount">
		/// amount
		/// </param>
		/// <returns>
		/// money
		/// </returns>
		public static Money Money(this ICurrency currency, decimal amount)
		{
			return new Money(amount, currency);
		}
	}
}

