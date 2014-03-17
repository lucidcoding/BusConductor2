using System;
using System.Collections.Generic;
using BusConductor.Domain.Entities;
using BusConductor.Domain.Enumerations;
using NUnit.Framework;

namespace BusConductor.Domain.UnitTests.Entities.BusTests
{
    [TestFixture]
    public class GetBookingStatusForTests
    {
        [Test]
        [TestCase(1, BusDayBookingStatus.Free)]
        [TestCase(2, BusDayBookingStatus.PendingPm)]
        [TestCase(3, BusDayBookingStatus.PendingAllDay)]
        [TestCase(6, BusDayBookingStatus.PendingAllDay)]
        [TestCase(7, BusDayBookingStatus.PendingAm)]
        [TestCase(8, BusDayBookingStatus.Free)]
        [TestCase(10, BusDayBookingStatus.ConfirmedPm)]
        [TestCase(11, BusDayBookingStatus.ConfirmedAllDay)]
        [TestCase(17, BusDayBookingStatus.ConfirmedAm | BusDayBookingStatus.ConfirmedPm)]
        [TestCase(18, BusDayBookingStatus.ConfirmedAllDay)]
        [TestCase(20, BusDayBookingStatus.ConfirmedAm | BusDayBookingStatus.PendingPm)]
        [TestCase(21, BusDayBookingStatus.PendingAllDay)]
        [TestCase(23, BusDayBookingStatus.PendingAm)]
        [TestCase(24, BusDayBookingStatus.Free)]
        public void ReturnsCorrectBookingStatus(int day, BusDayBookingStatus expectedBookingStatus)
        {
            var bus = new Bus();
            bus.Bookings = new List<Booking>();

            bus.Bookings.Add(new Booking
                                 {
                                     Bus = bus,
                                     PickUp = new DateTime(2000, 10, 2),
                                     DropOff = new DateTime(2000, 10, 7),
                                     Status = BookingStatus.Pending,
                                 });


            bus.Bookings.Add(new Booking
                                 {
                                     Bus = bus,
                                     PickUp = new DateTime(2000, 10, 10),
                                     DropOff = new DateTime(2000, 10, 17),
                                     Status = BookingStatus.Confirmed,
                                 });

            bus.Bookings.Add(new Booking
                                 {
                                     Bus = bus,
                                     PickUp = new DateTime(2000, 10, 17),
                                     DropOff = new DateTime(2000, 10, 20),
                                     Status = BookingStatus.Confirmed,
                                 });

            bus.Bookings.Add(new Booking
                                 {
                                     Bus = bus,
                                     PickUp = new DateTime(2000, 10, 20),
                                     DropOff = new DateTime(2000, 10, 23),
                                     Status = BookingStatus.Pending,
                                 });

            var actualBookingStatus = bus.GetBookingStatusFor(new DateTime(2000, 10, day));
            Assert.That(actualBookingStatus, Is.EqualTo(expectedBookingStatus));
        }
    }
}
