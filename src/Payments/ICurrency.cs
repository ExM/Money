using System;
namespace Payments
{
	public interface ICurrency
	{
		
		string CharCode {get;}
		
		int NumCode {get;}
		
		string Symbol {get;}
		
		decimal MinorUnit {get;}
	}
}

