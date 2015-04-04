using System;

namespace AbbyyLS.Payments
{
	/// <summary>
	/// This structure provides a amount of money in some currency
	/// </summary>
	public struct Money
	{
		/// <summary>
		/// amount of money
		/// </summary>
		public readonly decimal Amount;
		
		/// <summary>
		/// currency
		/// </summary>
		public readonly ICurrency Currency;
		
		/// <summary>
		/// creates a amount of money in any currency
		/// </summary>
		/// <param name="amount">
		/// amount of money
		/// </param>
		/// <param name="currency">
		/// currency
		/// </param>
		public Money(decimal amount, ICurrency currency)
		{
			if(currency == null)
				throw new ArgumentNullException("currency");
			Amount = amount;
			Currency = currency;
		}
		
		/// <summary>
		/// show amount and character code of currency
		/// </summary>
		public override string ToString()
		{
			return string.Format ("{0:G} {1}", Amount, Currency.CharCode);
		}
		
		/// <summary>
		/// contains an integer number of currency units
		/// </summary>
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
		
		/// <summary>
		/// the total number minot of unit of currency
		/// </summary>
		public decimal TotalMinorUnit
		{
			get
			{
				if(Currency.MinorUnit == 0m)
					throw new InvalidOperationException(string.Format("undefined minor unit in {0} currency", Currency.CharCode));
				return Amount/Currency.MinorUnit;
			}
		}
		
		/// <summary>
		/// Returns the largest integer less than or equal to minor of unit of this money.
		/// </summary>
		/// <returns>
		/// a new instance with the required amount or the current instance if the operation is not possible
		/// </returns>
		public Money FloorMinorUnit()
		{
			if(Currency.MinorUnit == 0m)
				return this;
			return new Money(decimal.Floor(Amount/Currency.MinorUnit)*Currency.MinorUnit, Currency);
		}
		
		/// <summary>
		/// Returns the largest integer less than or equal to this money.
		/// </summary>
		/// <returns>
		/// a new instance with the required amount or the current instance if the operation is not possible
		/// </returns>
		public Money FloorMajorUnit()
		{
			return new Money(decimal.Floor(Amount), Currency);
		}
		
		/// <summary>
		/// Returns the smallest integral value that is greater than or equal to minor of unit of this money.
		/// </summary>
		/// <returns>
		/// a new instance with the required amount or the current instance if the operation is not possible
		/// </returns>
		public Money CeilingMinorUnit()
		{
			if(Currency.MinorUnit == 0m)
				return this;
			return new Money(decimal.Ceiling(Amount/Currency.MinorUnit)*Currency.MinorUnit, Currency);
		}
		
		/// <summary>
		/// Returns the smallest integral value that is greater than or equal to this money.
		/// </summary>
		/// <returns>
		/// a new instance with the required amount or the current instance if the operation is not possible
		/// </returns>
		public Money CeilingMajorUnit()
		{
			return new Money(decimal.Ceiling(Amount), Currency);
		}
		
		/// <summary>
		/// mutiply amount
		/// </summary>
		public static Money Mult(decimal lhs, Money rhs)
		{
			return new Money(lhs * rhs.Amount, rhs.Currency);
		}
		
		/// <summary>
		/// mutiply amount
		/// </summary>
		public static Money Mult(Money lhs, decimal rhs)
		{
			return new Money(lhs.Amount * rhs, lhs.Currency);
		}
		
		/// <summary>
		/// divide amount
		/// </summary>
		public static Money Div(Money lhs, decimal rhs)
		{
			return new Money(lhs.Amount / rhs, lhs.Currency);
		}
		
		/// <summary>
		/// mutiply amount
		/// </summary>
		public static Money operator *(Money lhs, decimal rhs)
		{
			return Mult(lhs, rhs);
		}
		
		/// <summary>
		/// mutiply amount
		/// </summary>
		public static Money operator *(decimal lhs, Money rhs)
		{
			return Mult(lhs, rhs);
		}
		
		/// <summary>
		/// divide amount
		/// </summary>
		public static Money operator /(Money lhs, decimal rhs)
		{
			return Div(lhs, rhs);
		}

		/// <summary>
		/// Determines whether the specified System.Object is equal to the current Money.
		/// </summary>
		public override bool Equals(object obj)
		{
			if (!(obj is Money))
				return false;

			return Equals((Money)obj);
		}

		/// <summary>
		/// Determines whether the specified Money is equal to the current Money.
		/// </summary>
		public bool Equals(Money other)
		{
			return Amount == other.Amount &&
				Currency == other.Currency;
		}

		/// <summary>
		/// Compares two Money structures.
		/// </summary>
		public static bool operator ==(Money x, Money y)
		{
			return x.Equals(y);
		}

		/// <summary>
		/// Compares two Money structures.
		/// </summary>
		public static bool operator !=(Money x, Money y)
		{
			return !x.Equals(y);
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return Amount.GetHashCode() ^ Currency.GetHashCode();
		}
	}
}

