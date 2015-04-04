using System;

namespace AbbyyLS.Payments
{
	/// <summary>
	/// interface providing information on currency
	/// </summary>
	public interface ICurrency
	{
		/// <summary>
		/// character code
		/// </summary>
		string CharCode {get;}
		
		/// <summary>
		/// number code
		/// </summary>
		int NumCode {get;}
		
		/// <summary>
		/// symbol of currency
		/// </summary>
		string Symbol {get;}
		
		/// <summary>
		/// minor of unit scale
		/// </summary>
		decimal MinorUnit {get;}
	}
}

