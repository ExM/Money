using System;


namespace Payments.Iso4217Currencies
{
	public class Currency: ICurrency
	{
		private readonly string _charCode;
		private readonly int _numCode;
		private readonly string _symbol;
		private readonly decimal _minorUnit;
		
		internal Currency(string charCode, string sym, int num, decimal mu)
		{
			_charCode = charCode;
			_numCode = num;
			_symbol = sym;
			_minorUnit = mu;
		}
		
		public override string ToString()
		{
			return string.Format ("[Currency]");
		}
	

		#region ICurrency implementation
		public string CharCode
		{
			get
			{
				return _charCode;
			}
		}

		public int NumCode
		{
			get
			{
				return _numCode;
			}
		}

		public string Symbol
		{
			get
			{
				return _symbol;
			}
		}

		public decimal MinorUnit
		{
			get
			{
				return _minorUnit;
			}
		}
		#endregion
	}
}

