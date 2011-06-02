using System;
using NUnit.Framework;
using System.Threading;
using System.Globalization;

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
		public void Equals()
		{
			ICurrency c1 = Iso4217.RUB;
			ICurrency c2 = Iso4217.AED;
			Assert.AreNotEqual(c1, c2);
			Assert.IsFalse(c1 == c2);

			ICurrency c3 = Iso4217.RUB;
			Assert.AreEqual(c1, c3);
			Assert.IsTrue(c1 == c3);

			UnknownCurrency uc = new UnknownCurrency("RUB");
			Assert.AreNotEqual(c1, uc);
			Assert.IsFalse(c1 == uc);
		}
		
		[Test]
		public void ViewUYU()
		{
			Assert.AreEqual("UYU", Iso4217.UYU.CharCode);
			Assert.AreEqual("$U", Iso4217.UYU.Symbol);
			Assert.AreEqual(858, Iso4217.UYU.NumCode);
			Assert.AreEqual(0.01m, Iso4217.UYU.MinorUnit);
		}

		[TestCase("USD", "ru-RU", "Доллар США")]
		[TestCase("USD", "en-GB", "US Dollar")]
		[TestCase("ALL", "ru-RU", "Лек")]
		[TestCase("ALL", "en-GB", "Lek")]
		[TestCase("XDR", "ru-RU", "Специальные права заимствования")]
		public void Localization(string code, string culture, string exp)
		{
			CultureInfo ci = CultureInfo.GetCultureInfo(culture);
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;
			Assert.AreEqual(exp, Iso4217.Parse(code).ToString());
		}
	}
}

