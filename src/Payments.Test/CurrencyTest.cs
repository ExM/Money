using System;
using NUnit.Framework;

namespace Payments.Test
{
	[TestFixture]
	public class CurrencyTest
	{
		[TestCase("USD", "USD")]
		[TestCase("XXX", "XXX")]
		[TestCase("???", null)]
		public void Parse(string code, string exp)
		{
			if(exp != null)
				Assert.AreEqual(exp, Iso4217.Parse(code).CharCode);
			else
				Assert.IsNull(Iso4217.Parse(code));
		}
		
		[Test]
		public void UYU()
		{
			Assert.AreEqual("UYU", Iso4217.UYU.CharCode);
			Assert.AreEqual("$U", Iso4217.UYU.Symbol);
			Assert.AreEqual(858, Iso4217.UYU.NumCode);
			Assert.AreEqual(0.01m, Iso4217.UYU.MinorUnit);
		}
	}
}

