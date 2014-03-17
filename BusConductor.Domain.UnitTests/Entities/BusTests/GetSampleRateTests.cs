using System.Collections.Generic;
using BusConductor.Domain.Entities;
using NUnit.Framework;

namespace BusConductor.Domain.UnitTests.Entities.BusTests
{
    [TestFixture]
    public class GetSampleRateTests
    {
        private Bus _bus;

        [SetUp]
        public void SetUp()
        {
            _bus = new Bus();
            _bus.PricingPeriods = new List<PricingPeriod>();
            var pricingPeriod1 = new PricingPeriod();
            pricingPeriod1.StartMonth = 1;
            pricingPeriod1.StartDay = 1;
            pricingPeriod1.EndMonth = 4;
            pricingPeriod1.EndDay = 30;
            pricingPeriod1.FridayToFridayRate = 800;
            pricingPeriod1.FridayToMondayRate = 450;
            pricingPeriod1.MondayToFridayRate = 500;
            pricingPeriod1.Bus = _bus;
            _bus.PricingPeriods.Add(pricingPeriod1);
            var pricingPeriod2 = new PricingPeriod();
            pricingPeriod2.StartMonth = 5;
            pricingPeriod2.StartDay = 1;
            pricingPeriod2.EndMonth = 8;
            pricingPeriod2.EndDay = 31;
            pricingPeriod2.FridayToFridayRate = 1000;
            pricingPeriod2.FridayToMondayRate = 550;
            pricingPeriod2.MondayToFridayRate = 600;
            pricingPeriod2.Bus = _bus;
            _bus.PricingPeriods.Add(pricingPeriod2);
            var pricingPeriod3 = new PricingPeriod();
            pricingPeriod3.StartMonth = 9;
            pricingPeriod3.StartDay = 1;
            pricingPeriod3.EndMonth = 12;
            pricingPeriod3.EndDay = 31;
            pricingPeriod3.FridayToFridayRate = 700;
            pricingPeriod3.FridayToMondayRate = 400;
            pricingPeriod3.MondayToFridayRate = 450;
            pricingPeriod3.Bus = _bus;
            _bus.PricingPeriods.Add(pricingPeriod3);
        }

        [Test]
        public void ReturnsCorrectWinterMondayToFriday()
        {
            var rate = _bus.GetWinterMondayToFridayRate();
            Assert.That(rate, Is.EqualTo(500));
        }

        [Test]
        public void ReturnsCorrectWinterFridayToMonday()
        {
            var rate = _bus.GetWinterFridayToMondayRate();
            Assert.That(rate, Is.EqualTo(450));
        }

        [Test]
        public void ReturnsCorrectWinterFridayToFriday()
        {
            var rate = _bus.GetWinterFridayToFridayRate();
            Assert.That(rate, Is.EqualTo(800));
        }

        [Test]
        public void ReturnsCorrectSpringMondayToFriday()
        {
            var rate = _bus.GetSpringMondayToFridayRate();
            Assert.That(rate, Is.EqualTo(500));
        }

        [Test]
        public void ReturnsCorrectSpringFridayToMonday()
        {
            var rate = _bus.GetSpringFridayToMondayRate();
            Assert.That(rate, Is.EqualTo(450));
        }

        [Test]
        public void ReturnsCorrectSpringFridayToFriday()
        {
            var rate = _bus.GetSpringFridayToFridayRate();
            Assert.That(rate, Is.EqualTo(800));
        }

        [Test]
        public void ReturnsCorrectSummerMondayToFriday()
        {
            var rate = _bus.GetSummerMondayToFridayRate();
            Assert.That(rate, Is.EqualTo(600));
        }

        [Test]
        public void ReturnsCorrectSummerFridayToMonday()
        {
            var rate = _bus.GetSummerFridayToMondayRate();
            Assert.That(rate, Is.EqualTo(550));
        }

        [Test]
        public void ReturnsCorrectSummerFridayToFriday()
        {
            var rate = _bus.GetSummerFridayToFridayRate();
            Assert.That(rate, Is.EqualTo(1000));
        }

        [Test]
        public void ReturnsCorrectAutumnMondayToFriday()
        {
            var rate = _bus.GetAutumnMondayToFridayRate();
            Assert.That(rate, Is.EqualTo(450));
        }

        [Test]
        public void ReturnsCorrectAutumnFridayToMonday()
        {
            var rate = _bus.GetAutumnFridayToMondayRate();
            Assert.That(rate, Is.EqualTo(400));
        }

        [Test]
        public void ReturnsCorrectAutumnFridayToFriday()
        {
            var rate = _bus.GetAutumnFridayToFridayRate();
            Assert.That(rate, Is.EqualTo(700));
        }
    }
}
