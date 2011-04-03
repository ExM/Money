using System;
namespace Payments
{
	public class UnknownCurrency: ICurrency
	{
		private readonly string _charCode;
		private readonly int _numCode;
		
		public UnknownCurrency(string charCode)
		{
			_charCode = charCode;
			_numCode = -1;
		}
		
		public UnknownCurrency(int numCode)
		{
			_charCode = null;
			_numCode = numCode;
		}
		
		public override string ToString()
		{
			if(_charCode == null)
				return _numCode.ToString();
			else
				return _charCode;
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

		public string Symbol {
			get
			{
				return "¤";
			}
		}

		public decimal MinorUnit
		{
			get
			{
				return 0;
			}
		}
		#endregion
}
}

