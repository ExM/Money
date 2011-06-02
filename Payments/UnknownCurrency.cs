using System;
namespace Payments
{
	/// <summary>
	/// unknowm currency
	/// </summary>
	public class UnknownCurrency: ICurrency
	{
		private readonly string _charCode;
		private readonly int _numCode;
		
		/// <summary>
		/// unknowm currency
		/// </summary>
		/// <param name="charCode">
		/// any code
		/// </param>
		public UnknownCurrency(string charCode)
		{
			_charCode = charCode;
			_numCode = -1;
		}
		
		/// <summary>
		/// unknowm currency
		/// </summary>
		/// <param name="numCode">
		/// any code
		/// </param>
		public UnknownCurrency(int numCode)
		{
			_charCode = null;
			_numCode = numCode;
		}
		
		/// <summary>
		/// character or number code
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public override string ToString()
		{
			if(_charCode == null)
				return _numCode.ToString();
			else
				return _charCode;
		}

		#region ICurrency implementation
		/// <summary>
		/// character code
		/// </summary>
		public string CharCode
		{
			get
			{
				return _charCode;
			}
		}
		
		/// <summary>
		/// number code
		/// </summary>
		public int NumCode
		{
			get
			{
				return _numCode;
			}
		}
		
		/// <summary>
		/// default symbol of currency
		/// </summary>
		public string Symbol {
			get
			{
				return "¤";
			}
		}
		
		/// <summary>
		/// no minor of unit
		/// </summary>
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

