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
		
		public bool IsRounded
		{
			get
			{
				if(Currency.MinorUnit == 0m)
					return true;
				decimal mu = Amount/Currency.MinorUnit;
				return decimal.Truncate(mu) == mu;
			}
		}
		
		public decimal TotalMinorUnit
		{
			get
			{
				if(Currency.MinorUnit == 0m)
					throw new InvalidOperationException(string.Format("undefined minor unit in {0} currency", Currency.CharCode));
				return Amount/Currency.MinorUnit;
			}
		}
		
		public Money FloorMinorUnit()
		{
			if(Currency.MinorUnit == 0m)
				return this;
			return new Money(decimal.Floor(Amount/Currency.MinorUnit)*Currency.MinorUnit, Currency);
		}
		
		public Money FloorMajorUnit()
		{
			return new Money(decimal.Floor(Amount), Currency);
		}
		
		public Money CeilingMinorUnit()
		{
			if(Currency.MinorUnit == 0m)
				return this;
			return new Money(decimal.Ceiling(Amount/Currency.MinorUnit)*Currency.MinorUnit, Currency);
		}
		
		public Money CeilingMajorUnit()
		{
			return new Money(decimal.Ceiling(Amount), Currency);
		}

	}
}

