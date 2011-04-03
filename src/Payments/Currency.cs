using System;
namespace Payments
{
	public class Currency
	{
		
		public readonly string CharCode;
		
		public readonly int NumCode;
		
		public readonly string Symbol;
		
		public readonly decimal MinorUnit;
		
		public Currency(string chCode, string sym, int num, decimal mu)
		{
			CharCode = chCode;
			NumCode = num;
			Symbol = sym;
			MinorUnit = mu;
		}
		
		public override string ToString()
		{
			return string.Format ("[Currency]");
		}
	}
}

