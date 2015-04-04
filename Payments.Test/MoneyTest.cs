using System;
using NUnit.Framework;
using System.Threading;
using System.Globalization;

namespace AbbyyLS.Payments
{
	[TestFixture]
	public class MoneyTest
	{
		[TestCase("USD", 20.95, true)]
		[TestCase("USD", 20.5, true)]
		[TestCase("USD", -20, true)]
		[TestCase("USD", -0.99, true)]
		[TestCase("USD", -0.991, false)]
		[TestCase("USD", 20.999, false)]
		[TestCase("BYR", 20, true)]
		[TestCase("BYR", 21, true)]
		[TestCase("BYR", 21.00001, false)]
		[TestCase("XAU", 21.00001, true)]
		public void IsRounded(string code, decimal amount, bool exp)
		{
			Assert.AreEqual(exp, Iso4217.Parse(code).Money(amount).IsRounded);
		}
		
		[TestCase("USD", 20.95, 2095)]
		[TestCase("USD", 20.5, 2050)]
		[TestCase("USD", -20, -2000)]
		[TestCase("USD", -0.99, -99)]
		[TestCase("USD", -0.991, -99.1)]
		[TestCase("USD", 20.999, 2099.9)]
		[TestCase("BYR", 20, 20)]
		[TestCase("BYR", 21, 21)]
		[TestCase("BYR", 21.00001, 21.00001)]
		public void TotalMinorUnit(string code, decimal amount, decimal exp)
		{
			Assert.AreEqual(exp, Iso4217.Parse(code).Money(amount).TotalMinorUnit);
		}
		
		[TestCase("USD", 20.95, 20.95)]
		[TestCase("USD", 20.953, 20.95)]
		[TestCase("USD", -20.953, -20.96)]
		[TestCase("BYR", 20, 20)]
		[TestCase("BYR", 20.1, 20)]
		[TestCase("BYR", 20.00001, 20)]
		[TestCase("BYR", -20.00001, -21)]
		public void FloorMinorUnit(string code, decimal amount, decimal exp)
		{
			Assert.AreEqual(exp, Iso4217.Parse(code).Money(amount).FloorMinorUnit().Amount);
		}
		
		[TestCase("USD", 20.95, 20.95)]
		[TestCase("USD", 20.953, 20.96)]
		[TestCase("USD", -20.953, -20.95)]
		[TestCase("BYR", 20, 20)]
		[TestCase("BYR", 20.1, 21)]
		[TestCase("BYR", 20.00001, 21)]
		[TestCase("BYR", -20.00001, -20)]
		public void CeilingMinorUnit(string code, decimal amount, decimal exp)
		{
			Assert.AreEqual(exp, Iso4217.Parse(code).Money(amount).CeilingMinorUnit().Amount);
		}
		
		[TestCase("USD", 20, 20)]
		[TestCase("USD", 20.953, 20)]
		[TestCase("USD", -20.953, -21)]
		[TestCase("BYR", 20, 20)]
		[TestCase("BYR", 20.1, 20)]
		[TestCase("BYR", 20.00001, 20)]
		[TestCase("BYR", -20.00001, -21)]
		public void FloorMajorUnit(string code, decimal amount, decimal exp)
		{
			Assert.AreEqual(exp, Iso4217.Parse(code).Money(amount).FloorMajorUnit().Amount);
		}
		
		[TestCase("USD", 20, 20)]
		[TestCase("USD", 20.953, 21)]
		[TestCase("USD", -20.953, -20)]
		[TestCase("BYR", 20, 20)]
		[TestCase("BYR", 20.1, 21)]
		[TestCase("BYR", 20.00001, 21)]
		[TestCase("BYR", -20.00001, -20)]
		public void CeilingMajorUnit(string code, decimal amount, decimal exp)
		{
			Assert.AreEqual(exp, Iso4217.Parse(code).Money(amount).CeilingMajorUnit().Amount);
		}
		
		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void NoTotalMinorUnit()
		{
			decimal mu = Iso4217.XAU.Money(99.99m).TotalMinorUnit;
			Assert.Fail("minor unit exist: {0}", mu);
		}
		
		[Test]
		public void Equal()
		{
			Assert.AreEqual(Iso4217.RUB.Money(1.23m), Iso4217.RUB.Money(1.23m));
			Assert.AreNotEqual(Iso4217.RUB.Money(1.24m), Iso4217.RUB.Money(1.23m));
			Assert.AreNotEqual(Iso4217.RUB.Money(1.23m), Iso4217.USD.Money(1.23m));

			Assert.IsTrue(Iso4217.RUB.Money(1.23m) == Iso4217.RUB.Money(1.23m));
			Assert.IsFalse(Iso4217.RUB.Money(1.23m) != Iso4217.RUB.Money(1.23m));

			Assert.IsTrue(Iso4217.RUB.Money(1.23m) != Iso4217.USD.Money(1.23m));
			Assert.IsTrue(Iso4217.RUB.Money(1.24m) != Iso4217.RUB.Money(1.23m));
		}
		
		[Test]
		public void Operatorsl()
		{
			Assert.AreEqual(Iso4217.RUB.Money(2.46m), Iso4217.RUB.Money(1.23m) * 2);
			Assert.AreEqual(Iso4217.RUB.Money(2.46m), 2 * Iso4217.RUB.Money(1.23m));
			Assert.AreEqual(Iso4217.RUB.Money(1.23m), Iso4217.RUB.Money(2.46m) / 2);
		}
		
		[Test]
		public void Show()
		{
			CultureInfo ci = CultureInfo.InvariantCulture;
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;
			
			Assert.AreEqual("1.23 RUB", Iso4217.RUB.Money(1.23m).ToString());
			Assert.AreEqual("1.123456789012345678900876523 USD", Iso4217.USD.Money(1.123456789012345678900876523m).ToString());
		}
	}
}

