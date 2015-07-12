using System;
using NUnit.Framework;
using System.Threading;
using System.Globalization;

namespace AbbyyLS.Payments
{
	[TestFixture]
	public class MoneyTest
	{
		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void InvalidArgsInConstructor()
		{
			var mu = new Money(99.99m, null);
		}

		[TestCase("USD", -0.991)]
		[TestCase("USD", 20.999)]
		[TestCase("BYR", 21.00001)]
		public void NotRounded(string code, decimal amount)
		{
			Assert.IsFalse(Iso4217.Parse(code).Money(amount).IsRounded);
		}

		[TestCase("USD", 20.95)]
		[TestCase("USD", 20.5)]
		[TestCase("USD", -20)]
		[TestCase("USD", -0.99)]
		[TestCase("BYR", 20)]
		[TestCase("BYR", 21)]
		[TestCase("XAU", 21.00001)]
		public void IsRounded(string code, decimal amount)
		{
			Assert.IsTrue(Iso4217.Parse(code).Money(amount).IsRounded);
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
		[TestCase("XDR", 20.00001, 20.00001)]
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
		[TestCase("XDR", 20.00001, 20.00001)]
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
			var mu = Iso4217.XAU.Money(99.99m).TotalMinorUnit;
		}
		
		[Test]
		public void Equal()
		{
			var m1 = Iso4217.RUB.Money(1.23m);
			var m1E = Iso4217.RUB.Money(1.23m);
			var m2 = Iso4217.RUB.Money(1.24m);
			var m3 = Iso4217.USD.Money(1.23m);

			Assert.AreEqual(m1, m1);
			Assert.AreNotEqual(m2, m1);
			Assert.AreNotEqual(m1, m3);

			Assert.IsTrue(m1 == m1E);
			Assert.IsFalse(m1 != m1E);

			Assert.IsTrue(m1 != m3);
			Assert.IsTrue(m2 != m1);

			Assert.IsFalse(m1.Equals((object)m3));
			Assert.IsFalse(m1.Equals((object)"123"));
			Assert.IsTrue(m1.Equals((object)m1E));
		}
		
		[Test]
		public void Division()
		{
			Assert.AreEqual(Iso4217.RUB.Money(1.23m), Iso4217.RUB.Money(2.46m) / 2);
		}

		[Test]
		public void Multiply()
		{
			Assert.AreEqual(Iso4217.RUB.Money(2.46m), Iso4217.RUB.Money(1.23m) * 2);
			Assert.AreEqual(Iso4217.RUB.Money(2.46m), 2 * Iso4217.RUB.Money(1.23m));
		}

		[Test]
		public void MultiplyWithZero()
		{
			Assert.AreEqual(Iso4217.RUB, (Iso4217.RUB.Money(1.23m) * 0).Currency);
			Assert.AreEqual(Iso4217.RUB, (0 * Iso4217.RUB.Money(1.23m)).Currency);
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

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void InvalidMatch()
		{
			var l = Iso4217.EUR.Money(1.23m);
			var r = Iso4217.RUB.Money(2.23m);

			var res = l < r;
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void InvalidMatchZero()
		{
			var l = Iso4217.EUR.Money(0m);
			var r = Iso4217.RUB.Money(0m);

			var res = l < r;
		}

		[Test]
		public void MatchOperators()
		{
			var l = Iso4217.RUB.Money(1.23m);
			var r = Iso4217.RUB.Money(2.23m);
			var rE = Iso4217.RUB.Money(2.23m);

			Assert.IsTrue(l < r);
			Assert.IsTrue(l <= r);
			Assert.IsTrue(r <= rE);

			Assert.IsFalse(r < l);
			Assert.IsFalse(r <= l);

			Assert.IsFalse(l > r);
			Assert.IsFalse(l >= r);
			Assert.IsTrue(r >= rE);

			Assert.IsTrue(r > l);
			Assert.IsTrue(r >= l);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void InvalidAdditional()
		{
			var m1 = Iso4217.EUR.Money(1m);
			var m2 = Iso4217.RUB.Money(2m);

			var res = m1 + m2;
		}

		[Test]
		public void Additional()
		{
			var m1 = Iso4217.EUR.Money(1m);
			var m2 = Iso4217.EUR.Money(2m);
			var m3 = Iso4217.EUR.Money(3m);

			Assert.AreEqual(m3, m1 + m2);
		}

		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void InvalidSubtract()
		{
			var m1 = Iso4217.EUR.Money(1m);
			var m2 = Iso4217.RUB.Money(2m);

			var res = m1 - m2;
		}

		[Test]
		public void Subtract()
		{
			var m1 = Iso4217.EUR.Money(1m);
			var m2 = Iso4217.EUR.Money(2m);
			var m3 = Iso4217.EUR.Money(-1m);

			Assert.AreEqual(m3, m1 - m2);

			Assert.AreEqual(Iso4217.EUR, (m1 - m1).Currency);
		}

		[Test]
		public void HashCode()
		{
			var m1 = Iso4217.RUB.Money(1m).GetHashCode();
			var m1E = Iso4217.RUB.Money(1m).GetHashCode();
			var m2 = Iso4217.EUR.Money(2m).GetHashCode();
			var m3 = Iso4217.EUR.Money(-1m).GetHashCode();

			Assert.AreEqual(m1, m1E);
			Assert.AreNotEqual(m1, m2);
			Assert.AreNotEqual(m3, m2);
		}
	}
}

