using System;
using System.Resources;
using System.Globalization;

namespace AbbyyLS.Payments
{
	internal class Iso4217Currency: ICurrency
	{
		private static readonly ResourceManager _rMan = new ResourceManager("AbbyyLS.Payments.Dic", typeof(Iso4217Currency).Assembly);
		
		private readonly string _charCode;
		private readonly int _numCode;
		private readonly string _symbol;
		private readonly decimal _minorUnit;
		
		internal Iso4217Currency(string charCode, string sym, int num, decimal mu)
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

