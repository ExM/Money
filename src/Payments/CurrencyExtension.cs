using System;
namespace Payments
{
	public static class CurrencyExtension
	{
		public static Money Money(this ICurrency currency, decimal amount)
		{
			return new Money(amount, currency);
		}
	}
}

