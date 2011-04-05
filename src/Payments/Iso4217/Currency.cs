using System;
using System.Resources;


namespace Payments.Iso4217Currencies
{
	public class Currency: ICurrency
	{
		private static readonly ResourceManager _rMan = new ResourceManager("Payments.Dic", typeof(Currency).Assembly);
		
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
			return _rMan.GetString(_charCode);
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

