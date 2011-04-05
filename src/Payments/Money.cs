using System;

namespace Payments
{
	public struct Money
	{
		
		public readonly decimal Amount;
		
		public readonly ICurrency Currency;
		
		public Money(decimal amount, ICurrency currency)
		{
			Amount = amount;
			Currency = currency;
			
		}
	}
}

