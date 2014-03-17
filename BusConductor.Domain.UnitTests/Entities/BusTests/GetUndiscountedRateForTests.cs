using System;
using System.Collections.Generic;
using BusConductor.Domain.Entities;
using NUnit.Framework;

namespace BusConductor.Domain.UnitTests.Entities.BusTests
{
    [TestFixture]
    public class GetUndiscountedRateForTests
    {
        [Test]
        [TestCase(2000, 1, 3, 2000, 1, 7, 500)]
        [TestCase(2000, 1, 7, 2000, 1, 10, 450)]
        [TestCase(2000, 1, 7, 2000, 1, 14, 800)]
        [TestCase(2000, 5, 5, 2000, 5, 8, 550)]
        [TestCase(2000, 5, 5, 2000, 5, 12, 1000)]
        [TestCase(2000, 5, 1, 2000, 5, 5, 600)]
        [TestCase(2000, 5, 5, 2000, 5, 8, 550)]
        [TestCase(2000, 8, 14, 2000, 8, 21, 1150)]
        [TestCase(2000, 8, 14, 2000, 8, 25, 1600)]
        [TestCase(2002, 4, 29, 2002, 5, 3, 500)]
        [TestCase(2002, 4, 26, 2002, 5, 3, 800)]
        [TestCase(2002, 4, 26, 2002, 5, 6, 1350)]
        public void ReturnsCorrectRate(
            int pickUpYear, 
            int pickUpMonth, 
            int pickUpDay, 
            int dropOffYear,
            int dropOffMonth, 
            int dropOffDay, 
            decimal expectedRate)
        {
            var bus = new Bus();
            bus.PricingPeriods = new List<PricingPeriod>();
            var pricingPeriod1 = new PricingPeriod();
            pricingPeriod1.StartMonth = 1;
            pricingPeriod1.StartDay = 1;
            pricingPeriod1.EndMonth = 4;
            pricingPeriod1.EndDay = 30;
            pricingPeriod1.FridayToFridayRate = 800;
            pricingPeriod1.FridayToMondayRate = 450;
            pricingPeriod1.MondayToFridayRate = 500;
            pricingPeriod1.Bus = bus;
            bus.PricingPeriods.Add(pricingPeriod1);
            var pricingPeriod2 = new PricingPeriod();
            pricingPeriod2.StartMonth = 5;
            pricingPeriod2.StartDay = 1;
            pricingPeriod2.EndMonth = 8;
            pricingPeriod2.EndDay = 31;
            pricingPeriod2.FridayToFridayRate = 1000;
            pricingPeriod2.FridayToMondayRate = 550;
            pricingPeriod2.MondayToFridayRate = 600;
            pricingPeriod2.Bus = bus;
            bus.PricingPeriods.Add(pricingPeriod2);
            var pricingPeriod3 = new PricingPeriod();
            pricingPeriod3.StartMonth = 9;
            pricingPeriod3.StartDay = 1;
            pricingPeriod3.EndMonth = 12;
            pricingPeriod3.EndDay = 31;
            pricingPeriod3.FridayToFridayRate = 700;
            pricingPeriod3.FridayToMondayRate = 400;
            pricingPeriod3.MondayToFridayRate = 450;
            pricingPeriod3.Bus = bus;
            bus.PricingPeriods.Add(pricingPeriod3);
            var actualRate = bus.GetUndiscountedRateFor(new DateTime(pickUpYear, pickUpMonth, pickUpDay), new DateTime(dropOffYear, dropOffMonth, dropOffDay));
            Assert.That(actualRate, Is.EqualTo(expectedRate));
        }
    }
}
