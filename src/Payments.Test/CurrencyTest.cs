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
		public void UYU()
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
		public void Localization(string code, string culture, string exp)
		{
			CultureInfo ci = CultureInfo.GetCultureInfo(culture);
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;
			Assert.AreEqual(exp, Iso4217.Parse(code).ToString());
		}
	}
}

