using System;
using System.Collections.Generic;
using System.Linq;
using BusConductor.Domain.Entities;
using BusConductor.Domain.ParameterSets;
using BusConductor.Domain.ParameterSets.Booking;
using NUnit.Framework;

namespace BusConductor.Domain.UnitTests.Entities.BookingTests
{
    [TestFixture]
    public class ValidateCustomerMakeTests
    {
        private CustomerMakeBookingParameterSet _parameterSet;

        [SetUp]
        public void SetUp()
        {
            _parameterSet = new CustomerMakeBookingParameterSet();
            _parameterSet.PickUp = new DateTime(2090, 6, 12);
            _parameterSet.DropOff = new DateTime(2090, 6, 19);
            _parameterSet.Bus = new Bus {Id = Guid.NewGuid(), Bookings = new List<Booking>()};
            //_parameterSet.GuestRole = new Role {Id = Guid.NewGuid()};
            _parameterSet.Forename = "Brian";
            _parameterSet.Surname = "Blue";
            _parameterSet.AddressLine1 = "9 Green Lane";
            _parameterSet.AddressLine2 = "Blackthrope";
            _parameterSet.Town = "Orangeborough";
            _parameterSet.County = "Purpleshire";
            _parameterSet.PostCode = "M11AA";
            _parameterSet.Email = "brian.blue@isp.com";
            _parameterSet.TelephoneNumber = "07777777777";
            _parameterSet.IsMainDriver = true;
            _parameterSet.DrivingLicenceNumber = "XXX99999";
            _parameterSet.NumberOfAdults = 2;
            _parameterSet.NumberOfChildren = 2;
            _parameterSet.VoucherCode = "ABC123";
            _parameterSet.Voucher = new Voucher {Id = Guid.NewGuid(), Code = _parameterSet.VoucherCode, Discount = 10};
            _parameterSet.TermsAndConditionsAccepted = true;
            _parameterSet.RestrictionsAccepted = true;
            _parameterSet.CurrentUser = new User {Id = Guid.NewGuid()};
        }

        [Test]
        public void ValidBookingReturnsNoErrors()
        {
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(0));
        }

        [Test]
        public void ValidBookingSpecifyingAnotherDriverReturnsNoErrors()
        {
            _parameterSet.IsMainDriver = false;
            _parameterSet.DriverForename = "Rachel";
            _parameterSet.DriverSurname = "Red";
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(0));
        }

        [Test]
        public void MissingFieldsReturnValidationErrors()
        {
            _parameterSet.PickUp = null;
            _parameterSet.DropOff = null;
            _parameterSet.Bus = null;
            _parameterSet.Forename = null;
            _parameterSet.Surname = null;
            _parameterSet.AddressLine1 = null;
            _parameterSet.Town = null;
            _parameterSet.PostCode = null;
            _parameterSet.Email = null;
            _parameterSet.TelephoneNumber = null;
            _parameterSet.DrivingLicenceNumber = null;
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(11));
            Assert.That(validationMessages.Any(x => x.Field == "PickUp" && x.Text == "Pick up date is required."));
            Assert.That(validationMessages.Any(x => x.Field == "DropOff" && x.Text == "Drop off date is required."));
            Assert.That(validationMessages.Any(x => x.Field == "Bus" && x.Text == "Bus is required."));
            Assert.That(validationMessages.Any(x => x.Field == "Forename" && x.Text == "Forename is required."));
            Assert.That(validationMessages.Any(x => x.Field == "Surname" && x.Text == "Surname is required."));
            Assert.That(validationMessages.Any(x => x.Field == "AddressLine1" && x.Text == "Address line 1 is required."));
            Assert.That(validationMessages.Any(x => x.Field == "Town" && x.Text == "Town is required."));
            Assert.That(validationMessages.Any(x => x.Field == "PostCode" && x.Text == "Post code is required."));
            Assert.That(validationMessages.Any(x => x.Field == "Email" && x.Text == "Email is required."));
            Assert.That(validationMessages.Any(x => x.Field == "TelephoneNumber" && x.Text == "Telephone number is required."));
            Assert.That(validationMessages.Any(x => x.Field == "DrivingLicenceNumber" && x.Text == "Driving licence number is required."));
        }

        [Test]
        public void ReturnsErrorIfTermsAndConditionsOrRestrictionsAreNotAccepted()
        {
            _parameterSet.TermsAndConditionsAccepted = false;
            _parameterSet.RestrictionsAccepted = false;
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(2));
            Assert.That(validationMessages.Any(x => x.Field == "TermsAndConditionsAccepted" && x.Text == "You must accept the Terms and Conditions."));
            Assert.That(validationMessages.Any(x => x.Field == "RestrictionsAccepted" && x.Text == "Please check this box. If your trip does not meet these restrictions, please contact us directly to make a booking."));
        }
        [Test]
        public void FieldsThatAreTooLongReturnValidationErrors()
        {
            _parameterSet.Forename = new string('x', 51);
            _parameterSet.Surname = new string('x', 51);
            _parameterSet.AddressLine1 = new string('x', 51);
            _parameterSet.AddressLine2 = new string('x', 51);
            _parameterSet.AddressLine3 = new string('x', 51);
            _parameterSet.Town = new string('x', 51);
            _parameterSet.County = new string('x', 51);
            _parameterSet.Email = new string('x', 51);
            _parameterSet.TelephoneNumber = new string('0', 51); 
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(9));
            Assert.That(validationMessages.Any(x => x.Field == "Forename" && x.Text == "Forename must be 50 characters or less."));
            Assert.That(validationMessages.Any(x => x.Field == "Surname" && x.Text == "Surname must be 50 characters or less."));
            Assert.That(validationMessages.Any(x => x.Field == "AddressLine1" && x.Text == "Address line 1 must be 50 characters or less."));
            Assert.That(validationMessages.Any(x => x.Field == "AddressLine2" && x.Text == "Address line 2 must be 50 characters or less."));
            Assert.That(validationMessages.Any(x => x.Field == "AddressLine3" && x.Text == "Address line 3 must be 50 characters or less."));
            Assert.That(validationMessages.Any(x => x.Field == "Town" && x.Text == "Town must be 50 characters or less."));
            Assert.That(validationMessages.Any(x => x.Field == "County" && x.Text == "County must be 50 characters or less."));
            Assert.That(validationMessages.Any(x => x.Field == "Email" && x.Text == "Email must be 50 characters or less."));
            Assert.That(validationMessages.Any(x => x.Field == "TelephoneNumber" && x.Text == "Telephone number must be 50 characters or less."));
        }

        [Test]
        public void ReturnsErrorIfPickUpOrDropOffIsDefault()
        {
            _parameterSet.PickUp = new DateTime();
            _parameterSet.DropOff = new DateTime();
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(2));
            Assert.That(validationMessages.Any(x => x.Field == "PickUp" && x.Text == "Pick up date is required."));
            Assert.That(validationMessages.Any(x => x.Field == "DropOff" && x.Text == "Drop off date is required."));
        }
        
        [Test]
        public void ReturnsErrorIfPickUpOrDropOffIsInThePast()
        {
            _parameterSet.PickUp = new DateTime(2000, 1, 3);
            _parameterSet.DropOff = new DateTime(2000, 1, 7);
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(2));
            Assert.That(validationMessages.Any(x => x.Field == "PickUp" && x.Text == "Pick up date must not be in the past."));
            Assert.That(validationMessages.Any(x => x.Field == "DropOff" && x.Text == "Drop off date must not be in the past."));
        }

        [Test]
        public void ReturnsErrorIfDropOffIsBeforePickUp()
        {
            _parameterSet.PickUp = new DateTime(2090, 6, 19);
            _parameterSet.DropOff = new DateTime(2090, 6, 12);
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(1));
            Assert.That(validationMessages.Any(x => x.Field == "DropOff" && x.Text == "Drop off date must not be before pickup date.")); 
        }

        [Test]
        public void ReturnsErrorIfPickUpAndDropOffAreNotOnMondayOrFriday()
        {
            _parameterSet.PickUp = new DateTime(2090, 6, 13);
            _parameterSet.DropOff = new DateTime(2090, 6, 20);
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(2));
            Assert.That(validationMessages.Any(x => x.Field == "PickUp" && x.Text == "Pick up date must be a Monday or a Friday."));
            Assert.That(validationMessages.Any(x => x.Field == "DropOff" && x.Text == "Drop off date must be a Monday or a Friday."));
        }

        [Test]
        public void ReturnsErrorIfNumberOfAdultsIsZero()
        {
            _parameterSet.NumberOfAdults = 0;
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(1));
            Assert.That(validationMessages.Any(x => x.Field == "NumberOfAdults" && x.Text == "Booking must be for 1 or more adults.")); 
        }

        [Test]
        public void ReturnsErrorIfIsMainDriverIsFalseAndDriverForenameAndSurnameNotSet()
        {
            _parameterSet.IsMainDriver = false;
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(2));
            Assert.That(validationMessages.Any(x => x.Field == "DriverForename" && x.Text == "If you are not the main driver, the forename of the main driver is required."));
            Assert.That(validationMessages.Any(x => x.Field == "DriverSurname" && x.Text == "If you are not the main driver, the surname of the main driver is required."));
        }

        [Test]
        public void ReturnsErrorIfVoucherIsNotFound()
        {
            _parameterSet.Voucher = null;
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(1));
            Assert.That(validationMessages.Any(x => x.Field == "VoucherCode" && x.Text == "Voucher code is not valid."));
        }

        [Test]
        public void ReturnsErrorIfVoucherHasExpired()
        {
            _parameterSet.Voucher.ExpiryDate = DateTime.Now.AddDays(-2);
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(1));
            Assert.That(validationMessages.Any(x => x.Field == "VoucherCode" && x.Text == "Voucher code has expired."));
        }

        [Test]
        public void InvalidPostCodeReturnsError()
        {
            _parameterSet.PostCode = "Invalid";
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(1));
            Assert.That(validationMessages.Any(x => x.Field == "PostCode" && x.Text == "Post code is not valid."));
        }

        [Test]
        public void InvalidEmailReturnsError()
        {
            _parameterSet.Email = "Invalid";
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(1));
            Assert.That(validationMessages.Any(x => x.Field == "Email" && x.Text == "Email is not valid."));
        }

        [Test]
        public void ConflictingBookingReturnsError()
        {
            var booking = new Booking();
            booking.PickUp = new DateTime(2090, 6, 10);
            booking.DropOff = new DateTime(2090, 6, 14);
            booking.Bus = _parameterSet.Bus;
            _parameterSet.Bus.Bookings.Add(booking);
            var validationMessages = Booking.ValidateCustomerMake(_parameterSet);
            Assert.That(validationMessages.Count, Is.EqualTo(1));
            Assert.That(validationMessages.Any(x => x.Text == "Booking conflicts with existing bookings."));
        }
    }
}
