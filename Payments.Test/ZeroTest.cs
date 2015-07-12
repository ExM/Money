using System;
using NUnit.Framework;
using System.Threading;
using System.Globalization;

namespace AbbyyLS.Payments
{
	[TestFixture]
	public class ZeroTest
	{
		[Test]
		public void Create()
		{
			var mu = new Money(0m, null);

			Assert.IsTrue(mu == Money.Zero);
		}

		[Test]
		public void IsRounded()
		{
			Assert.IsTrue(Money.Zero.IsRounded);
		}
		
		[Test]
		public void TotalMinorUnit()
		{
			Assert.AreEqual(0, Money.Zero.TotalMinorUnit);
		}
		
		[Test]
		public void FloorMinorUnit()
		{
			Assert.AreEqual(Money.Zero, Money.Zero.FloorMinorUnit());
		}
		
		[Test]
		public void CeilingMinorUnit()
		{
			Assert.AreEqual(Money.Zero, Money.Zero.CeilingMinorUnit());
		}
		
		[Test]
		public void FloorMajorUnit()
		{
			Assert.AreEqual(Money.Zero, Money.Zero.FloorMajorUnit());
		}
		
		[Test]
		public void CeilingMajorUnit()
		{
			Assert.AreEqual(Money.Zero, Money.Zero.CeilingMajorUnit());
		}
		
		[Test]
		public void Equal()
		{
			Assert.AreEqual(Iso4217.RUB.Money(0m), Money.Zero);
			Assert.AreEqual(Money.Zero, Money.Zero);
		}
		
		[Test]
		public void Muliply()
		{
			var m = Iso4217.EUR.Money(1.23m);
			Assert.AreEqual(Money.Zero, Money.Zero * 2);
			Assert.AreEqual(Money.Zero, 2 * Money.Zero);
			Assert.AreEqual(Money.Zero, m * 0);
			Assert.AreEqual(Money.Zero, 0 * m);
		}

		[Test]
		public void Division()
		{
			Assert.AreEqual(Money.Zero, Money.Zero / 2);
		}

		[Test]
		public void Additional()
		{
			var m = Iso4217.EUR.Money(1.23m);
			Assert.AreEqual(Money.Zero, Money.Zero + Money.Zero);
			Assert.AreEqual(m, m + Money.Zero);
			Assert.AreEqual(m, Money.Zero + m);
		}

		[Test]
		public void Subtract()
		{
			var m = Iso4217.EUR.Money(1.23m);
			Assert.AreEqual(Money.Zero, Money.Zero - Money.Zero);
			Assert.AreEqual(Money.Zero, m - m);
			Assert.AreEqual(m, m - Money.Zero);
			Assert.AreEqual(-m.Amount, (Money.Zero - m).Amount);
		}
		
		[Test]
		public void Show()
		{
			CultureInfo ci = CultureInfo.InvariantCulture;
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;

			Assert.AreEqual("0", Money.Zero.ToString());
		}

		[Test]
		public void MatchOperators()
		{
			var l = Money.Zero;
			var lE = Money.Zero;
			var r = Iso4217.RUB.Money(2.23m);

			Assert.IsTrue(l < r);
			Assert.IsTrue(l <= r);
			Assert.IsTrue(l <= lE);

			Assert.IsFalse(r < l);
			Assert.IsFalse(r <= l);

			Assert.IsFalse(l > r);
			Assert.IsFalse(l >= r);
			Assert.IsTrue(l >= lE);

			Assert.IsTrue(r > l);
			Assert.IsTrue(r >= l);
		}

		[Test]
		public void HashCode()
		{
			var m1 = Money.Zero.GetHashCode();
			var m1E = new Money(0m, null).GetHashCode();
			var m2 = Iso4217.RUB.Money(1m).GetHashCode();
			var m3 = (Iso4217.RUB.Money(1m) * 0).GetHashCode();

			Assert.AreEqual(m1, m1E);
			Assert.AreNotEqual(m1, m2);
			Assert.AreNotEqual(m1, m3);
		}
	}
}

