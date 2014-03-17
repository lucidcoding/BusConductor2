using System;
using BusConductor.Application.Requests.Booking;
using BusConductor.UI.UnitTests.ControllerFactories;
using BusConductor.UI.ViewModels.Booking;
using Moq;
using NUnit.Framework;

namespace BusConductor.UI.UnitTests.Controllers.BookingControllerTests
{
    [TestFixture]
    public class ConfirmTests
    {
        private BookingControllerFactory _bookingControllerFactory;
        private ReviewViewModel _inViewModel;

        [SetUp]
        public void SetUp()
        {
            _bookingControllerFactory = new BookingControllerFactory();
            _inViewModel = new ReviewViewModel();
            _inViewModel.BusId = _bookingControllerFactory.BusId;
            _inViewModel.PickUp = new DateTime(2090, 1, 2);
            _inViewModel.DropOff = new DateTime(2090, 1, 6);
            _inViewModel.Forename = "Tom";
            _inViewModel.Surname = "Turquoise";
            _inViewModel.AddressLine1 = "1 Pink Street";
            _inViewModel.AddressLine2 = "Address 2";
            _inViewModel.AddressLine3 = "Address 3";
            _inViewModel.Town = "Blueborough";
            _inViewModel.County = "Purpleshire";
            _inViewModel.PostCode = "M1 1AA";
            _inViewModel.TelephoneNumber = "01234 567890";
            _inViewModel.Email = "tom@greenltd.com";
            _inViewModel.IsMainDriver = false;
            _inViewModel.DrivingLicenceNumber = "XYZ999";
            _inViewModel.DriverForename = "Pam";
            _inViewModel.DriverSurname = "Pink";
            _inViewModel.NumberOfAdults = 2;
            _inViewModel.NumberOfChildren = 2;
            _inViewModel.VoucherCode = "ABC111";
        }

        [Test]
        public void ConfirmCallsCorrectMethods()
        {
            _bookingControllerFactory.GetController().Confirm(_inViewModel);
            _bookingControllerFactory.BookingService.Verify(x => x.CustomerMake(It.IsAny<CustomerMakeBookingRequest>()));
        }

        [Test]
        public void ConfirmCreatesCorrectRequest()
        {
            CustomerMakeBookingRequest request = null;

            _bookingControllerFactory.BookingService
                .Setup(x => x.CustomerMake(It.IsAny<CustomerMakeBookingRequest>()))
                .Callback(delegate(CustomerMakeBookingRequest x) { request = x; });

            _bookingControllerFactory.GetController().Confirm(_inViewModel);
            Assert.That(request.BusId, Is.EqualTo(_inViewModel.BusId));
            Assert.That(request.PickUp, Is.EqualTo(_inViewModel.PickUp));
            Assert.That(request.DropOff, Is.EqualTo(_inViewModel.DropOff));
            Assert.That(request.Forename, Is.EqualTo(_inViewModel.Forename));
            Assert.That(request.Surname, Is.EqualTo(_inViewModel.Surname));
            Assert.That(request.AddressLine1, Is.EqualTo(_inViewModel.AddressLine1));
            Assert.That(request.AddressLine2, Is.EqualTo(_inViewModel.AddressLine2));
            Assert.That(request.AddressLine3, Is.EqualTo(_inViewModel.AddressLine3));
            Assert.That(request.Town, Is.EqualTo(_inViewModel.Town));
            Assert.That(request.County, Is.EqualTo(_inViewModel.County));
            Assert.That(request.PostCode, Is.EqualTo(_inViewModel.PostCode));
            Assert.That(request.Email, Is.EqualTo(_inViewModel.Email));
            Assert.That(request.TelephoneNumber, Is.EqualTo(_inViewModel.TelephoneNumber));
            Assert.That(request.IsMainDriver, Is.EqualTo(_inViewModel.IsMainDriver));
            Assert.That(request.DrivingLicenceNumber, Is.EqualTo(_inViewModel.DrivingLicenceNumber));
            Assert.That(request.DriverForename, Is.EqualTo(_inViewModel.DriverForename));
            Assert.That(request.DriverSurname, Is.EqualTo(_inViewModel.DriverSurname));
            Assert.That(request.NumberOfAdults, Is.EqualTo(_inViewModel.NumberOfAdults));
            Assert.That(request.NumberOfChildren, Is.EqualTo(_inViewModel.NumberOfChildren));
            Assert.That(request.VoucherCode, Is.EqualTo(_inViewModel.VoucherCode));
        }
    }
}
