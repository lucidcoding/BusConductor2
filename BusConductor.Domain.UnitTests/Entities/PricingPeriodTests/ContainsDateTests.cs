using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusConductor.Domain.Entities;
using NUnit.Framework;

namespace BusConductor.Domain.UnitTests.Entities.PricingPeriodTests
{
    [TestFixture]
    public class ContainsDateTests
    {
        [Test]
        [TestCase(2000, 1, 1)]
        [TestCase(2000, 1, 2)]
        [TestCase(2000, 2, 28)]
        [TestCase(2000, 2, 29)]
        [TestCase(2001, 2, 28)]
        public void ReturnsCorrectResultWhenPeriodIsAtStratOfYears(int year, int month, int day)
        {
            var pricingPeriod = new PricingPeriod();
            pricingPeriod.StartMonth = 1;
            pricingPeriod.StartDay = 1;
            pricingPeriod.EndMonth = 2;
            pricingPeriod.EndDay = 29;
            Assert.That(pricingPeriod.ContainsDate(new DateTime(year, month, day)));
        }

        [Test]
        [TestCase(2000, 4, 1)]
        [TestCase(2000, 4, 2)]
        [TestCase(2001, 6, 1)]
        [TestCase(2000, 8, 30)]
        [TestCase(2000, 8, 31)]
        public void ReturnsCorrectResultWhenPeriodIsWithinYear(int year, int month, int day)
        {
            var pricingPeriod = new PricingPeriod();
            pricingPeriod.StartMonth = 4;
            pricingPeriod.StartDay = 1;
            pricingPeriod.EndMonth = 8;
            pricingPeriod.EndDay = 31;
            Assert.That(pricingPeriod.ContainsDate(new DateTime(year, month, day)));
        }

        [Test]
        [TestCase(2000, 4, 1)]
        [TestCase(2000, 4, 2)]
        [TestCase(2001, 6, 1)]
        [TestCase(2000, 8, 30)]
        [TestCase(2000, 8, 31)]
        public void ReturnsCorrectResultWhenDateIsOutsidePeriod(int year, int month, int day)
        {
            var pricingPeriod = new PricingPeriod();
            pricingPeriod.StartMonth = 2;
            pricingPeriod.StartDay = 1;
            pricingPeriod.EndMonth = 3;
            pricingPeriod.EndDay = 31;
            Assert.That(pricingPeriod.ContainsDate(new DateTime(year, month, day)), Is.False);
        }

        [Test]
        [TestCase(1999, 11, 1)]
        [TestCase(1999, 11, 2)]
        [TestCase(2000, 1, 1)]
        [TestCase(2000, 1, 2)]
        [TestCase(2000, 2, 28)]
        [TestCase(2000, 2, 29)]
        [TestCase(2001, 2, 28)]
        public void ReturnsCorrectResultWhenPeriodIsAcrossYears(int year, int month, int day)
        {
            var pricingPeriod = new PricingPeriod();
            pricingPeriod.StartMonth = 11;
            pricingPeriod.StartDay = 1;
            pricingPeriod.EndMonth = 2;
            pricingPeriod.EndDay = 29;
            Assert.That(pricingPeriod.ContainsDate(new DateTime(year, month, day)));
        }

        [Test]
        [TestCase(1999, 3, 1)]
        [TestCase(1999, 3, 2)]
        [TestCase(1999, 10, 30)]
        [TestCase(1999, 10, 29)]
        [TestCase(2000, 3, 1)]
        [TestCase(2000, 3, 2)]
        [TestCase(2000, 10, 30)]
        [TestCase(2000, 10, 29)]


        [TestCase(2009, 3, 1)]
        [TestCase(2009, 3, 2)]
        [TestCase(2009, 10, 30)]
        [TestCase(2009, 10, 29)]
        [TestCase(2010, 3, 1)]
        [TestCase(2010, 3, 2)]
        [TestCase(2010, 10, 30)]
        [TestCase(2010, 10, 29)]
        public void ReturnsCorrectResultWhenPeriodIsAcrossYearsAndOutsideRange(int year, int month, int day)
        {
            var pricingPeriod = new PricingPeriod();
            pricingPeriod.StartMonth = 11;
            pricingPeriod.StartDay = 1;
            pricingPeriod.EndMonth = 2;
            pricingPeriod.EndDay = 29;
            Assert.That(pricingPeriod.ContainsDate(new DateTime(year, month, day)), Is.False);
        }
    }
}
