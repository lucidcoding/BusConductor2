using System;
using System.Collections.Generic;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.Enumerations;
using BusConductor.Domain.ParameterSets;
using BusConductor.Domain.ParameterSets.Booking;
using NUnit.Framework;

namespace BusConductor.Domain.UnitTests.Entities.BookingTests
{
    [TestFixture]
    public class AdminMakeWithBookingNumberTests
    {
        private AdminMakeBookingParameterSet _parameterSet;

        [SetUp]
        public void SetUp()
        {
            var applicationUser = new User { Id = Guid.NewGuid(), Username = "Application" };
            var guestRole = new Role { Id = Guid.NewGuid() };
            var voucher = new Voucher { Id = Guid.NewGuid(), Code = "ABC123", Discount = 10 };
            _parameterSet = new AdminMakeBookingParameterSet();
            _parameterSet.PickUp = new DateTime(2090, 6, 9);
            _parameterSet.DropOff = new DateTime(2090, 6, 16);
            _parameterSet.Bus = new Bus();
            _parameterSet.Bus.Id = Guid.NewGuid();
            _parameterSet.Bus.Bookings = new List<Booking>();
            _parameterSet.Bus.PricingPeriods = new List<PricingPeriod>();
            var pricingPeriod = new PricingPeriod();
            pricingPeriod.StartMonth = 1;
            pricingPeriod.StartDay = 1;
            pricingPeriod.EndMonth = 12;
            pricingPeriod.EndDay = 31;
            pricingPeriod.FridayToFridayRate = 600;
            _parameterSet.Bus.PricingPeriods.Add(pricingPeriod);
            _parameterSet.Forename = "Barry";
            _parameterSet.Surname = "Blue";
            _parameterSet.AddressLine1 = "99 Black Street";
            _parameterSet.AddressLine2 = "Purple District";
            _parameterSet.AddressLine3 = "Gray Area";
            _parameterSet.Town = "Greenville";
            _parameterSet.County = "Redshire";
            _parameterSet.PostCode = "M11AA";
            _parameterSet.Email = "barry.blue@isp.com";
            _parameterSet.TelephoneNumber = "0123456789";
            _parameterSet.IsMainDriver = true;
            _parameterSet.DrivingLicenceNumber = "XXX99999";
            _parameterSet.NumberOfAdults = 2;
            _parameterSet.NumberOfChildren = 0;
            _parameterSet.CurrentUser = applicationUser;
            _parameterSet.Status = BookingStatus.Confirmed;
            _parameterSet.TotalCost = 999;
            _parameterSet.CreatedOn = new DateTime(2013, 10, 1);
            _parameterSet.OtherBookingsToday = new List<Booking>();
        }

        [Test]
        public void CanMakePendingBookingWithBookingNumber()
        {
            var booking = Booking.AdminMakeWithBookingNumber(_parameterSet);
            Assert.That(booking.BookingNumber, Is.EqualTo("201310010001_Blue"));
            Assert.That(booking.PickUp, Is.EqualTo(_parameterSet.PickUp));
            Assert.That(booking.DropOff, Is.EqualTo(_parameterSet.DropOff));
            Assert.That(booking.NumberOfAdults, Is.EqualTo(_parameterSet.NumberOfAdults));
            Assert.That(booking.NumberOfChildren, Is.EqualTo(_parameterSet.NumberOfChildren));
            Assert.That(booking.Bus, Is.EqualTo(_parameterSet.Bus));
            Assert.That(booking.CreatedOn, Is.EqualTo(_parameterSet.CreatedOn));
            Assert.That(booking.CreatedBy, Is.EqualTo(_parameterSet.CurrentUser));
            Assert.That(booking.CreatedBy.Username, Is.EqualTo("Application"));
            Assert.That(booking.Customer.Forename, Is.EqualTo(_parameterSet.Forename));
            Assert.That(booking.Customer.Surname, Is.EqualTo(_parameterSet.Surname));
            Assert.That(booking.Customer.AddressLine1, Is.EqualTo(_parameterSet.AddressLine1));
            Assert.That(booking.Customer.AddressLine2, Is.EqualTo(_parameterSet.AddressLine2));
            Assert.That(booking.Customer.AddressLine3, Is.EqualTo(_parameterSet.AddressLine3));
            Assert.That(booking.Customer.Town, Is.EqualTo(_parameterSet.Town));
            Assert.That(booking.Customer.County, Is.EqualTo(_parameterSet.County));
            Assert.That(booking.Customer.PostCode, Is.EqualTo(_parameterSet.PostCode));
            Assert.That(booking.Customer.Email, Is.EqualTo(_parameterSet.Email));
            Assert.That(booking.Customer.TelephoneNumber, Is.EqualTo(_parameterSet.TelephoneNumber));
            Assert.That(booking.Status, Is.EqualTo(_parameterSet.Status));
            Assert.That(booking.TotalCost, Is.EqualTo(_parameterSet.TotalCost));
        }

        [Test]
        public void GetsNextBookingNumberIfBookingsExistThatDay()
        {
            _parameterSet.OtherBookingsToday.Add(new Booking { BookingNumber = "201310010001_Purple" });
            _parameterSet.OtherBookingsToday.Add(new Booking { BookingNumber = "201310010002_Black" });
            _parameterSet.OtherBookingsToday.Add(new Booking { BookingNumber = "201310010003_Mauve" });
            _parameterSet.OtherBookingsToday.Add(new Booking { BookingNumber = "201310010004_Red" });
            var booking = Booking.AdminMakeWithBookingNumber(_parameterSet);
            Assert.That(booking.BookingNumber, Is.EqualTo("201310010005_Blue"));
            
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void InvalidBookingThrowsException()
        {
            _parameterSet.Forename = null;
            Booking.AdminMakeWithBookingNumber(_parameterSet);
        }
    }
}
