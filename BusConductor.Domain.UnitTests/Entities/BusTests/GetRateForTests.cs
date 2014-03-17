//using System;
//using System.Collections.Generic;
//using BusConductor.Domain.Entities;
//using NUnit.Framework;

//namespace BusConductor.Domain.UnitTests.Entities.BusTests
//{
//    [TestFixture]
//    public class GetRateForTests
//    {
//        [Test]
//        [TestCase(1, 3, 70)]
//        [TestCase(1, 8, 75)]
//        [TestCase(3, 6, 80)]
//        [TestCase(3, 11, 85)]
//        [TestCase(6, 5, 90)]
//        [TestCase(6, 10, 100)]
//        [TestCase(9, 4, 80)]
//        [TestCase(9, 9, 85)]
//        public void ReturnsCorrectRate(int month, int day, decimal expectedRate)
//        {
//            var bus = new Bus();
//            bus.PricingPeriods = new List<PricingPeriod>();
//            bus.PricingPeriods.Add(new PricingPeriod());
//            bus.PricingPeriods[0].StartMonth = 1;
//            bus.PricingPeriods[0].StartDay = 1;
//            bus.PricingPeriods[0].EndMonth = 4;
//            bus.PricingPeriods[0].EndDay = 30;
//            bus.PricingPeriods[0].FridayToFridayRate = 800;
//            bus.PricingPeriods[0].FridayToMondayRate = 450;
//            bus.PricingPeriods[0].MondayToFridayRate = 500;
//            bus.PricingPeriods[0].Bus = bus;
//            bus.PricingPeriods.Add(new PricingPeriod());
//            bus.PricingPeriods[1].StartMonth = 5;
//            bus.PricingPeriods[1].StartDay = 1;
//            bus.PricingPeriods[1].EndMonth = 8;
//            bus.PricingPeriods[1].EndDay = 31;
//            bus.PricingPeriods[1].FridayToFridayRate = 1000;
//            bus.PricingPeriods[1].FridayToMondayRate = 550;
//            bus.PricingPeriods[1].MondayToFridayRate = 600;
//            bus.PricingPeriods[1].Bus = bus;
//            bus.PricingPeriods.Add(new PricingPeriod());
//            bus.PricingPeriods[2].StartMonth = 9;
//            bus.PricingPeriods[2].StartDay = 1;
//            bus.PricingPeriods[2].EndMonth = 12;
//            bus.PricingPeriods[2].EndDay = 31;
//            bus.PricingPeriods[2].FridayToFridayRate = 700;
//            bus.PricingPeriods[2].FridayToMondayRate = 400;
//            bus.PricingPeriods[2].MondayToFridayRate = 450;
//            bus.PricingPeriods[2].Bus = bus;
//            var actualRate = bus.GetRateFor(new DateTime(2000, month, day));
//            Assert.That(actualRate, Is.EqualTo(expectedRate));
//        }
//    }
//}
