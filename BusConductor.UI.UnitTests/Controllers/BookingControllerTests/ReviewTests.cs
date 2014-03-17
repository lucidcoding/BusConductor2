using System;
using System.Web.Mvc;
using BusConductor.Application.Requests.Booking;
using BusConductor.Domain.Entities;
using BusConductor.UI.UnitTests.ControllerFactories;
using BusConductor.UI.ViewModels.Booking;
using Moq;
using NUnit.Framework;

namespace BusConductor.UI.UnitTests.Controllers.BookingControllerTests
{
    [TestFixture]
    public class ReviewTests
    {
        private BookingControllerFactory _bookingControllerFactory;
        private MakeViewModel _inViewModel;
        private Mock<Booking> _booking;

        [SetUp]
        public void SetUp()
        {
            _bookingControllerFactory = new BookingControllerFactory();
            _inViewModel = new MakeViewModel();
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
            _inViewModel.ConfirmEmail = "tom@greenltd.com";
            _inViewModel.IsMainDriver = false;
            _inViewModel.DrivingLicenceNumber = "XYZ999";
            _inViewModel.DriverForename = "Pam";
            _inViewModel.DriverSurname = "Pink";
            _inViewModel.NumberOfAdults = 2;
            _inViewModel.NumberOfChildren = 2;
            _inViewModel.VoucherCode = "ABC111";
            _booking = new Mock<Booking>();
            _booking.SetupGet(x => x.PickUp).Returns(new DateTime(2090, 1, 2));
            _booking.SetupGet(x => x.DropOff).Returns(new DateTime(2090, 1, 6));
            _booking.SetupGet(x => x.IsMainDriver).Returns(false);
            _booking.SetupGet(x => x.DrivingLicenceNumber).Returns("XYZ999");
            _booking.SetupGet(x => x.DriverForename).Returns("Pam");
            _booking.SetupGet(x => x.DriverSurname).Returns("Pink");
            _booking.SetupGet(x => x.NumberOfAdults).Returns(2);
            _booking.SetupGet(x => x.NumberOfChildren).Returns(1);
            var bus = new Mock<Bus>();
            bus.SetupGet(x => x.Id).Returns(_bookingControllerFactory.BusId);
            _booking.SetupGet(x => x.Bus).Returns(bus.Object);
            var customer = new Mock<Customer>();
            customer.SetupGet(x => x.Forename).Returns("Tom");
            customer.SetupGet(x => x.Surname).Returns("Turquoise");
            customer.SetupGet(x => x.AddressLine1).Returns("1 Pink Street");
            customer.SetupGet(x => x.AddressLine2).Returns("Address 2");
            customer.SetupGet(x => x.AddressLine3).Returns("Address 3");
            customer.SetupGet(x => x.Town).Returns("Blueborough");
            customer.SetupGet(x => x.County).Returns("Purpleshire");
            customer.SetupGet(x => x.PostCode).Returns("M1 1AA");
            customer.SetupGet(x => x.TelephoneNumber).Returns("01234 567890");
            customer.SetupGet(x => x.Email).Returns("tom@greenltd.com");
            _booking.SetupGet(x => x.Customer).Returns(customer.Object);

            _bookingControllerFactory.BookingService
                .Setup(x => x.SummarizeCustomerMake(It.IsAny<CustomerMakeBookingRequest>()))
                .Returns(_booking.Object);
        }

        [Test]
        public void ReviewCallsCorrectMethods()
        {
            _bookingControllerFactory.GetController().Review(_inViewModel);
            _bookingControllerFactory.BookingService.Verify(x => x.ValidateCustomerMake(It.IsAny<CustomerMakeBookingRequest>()));
            _bookingControllerFactory.BookingService.Verify(x => x.SummarizeCustomerMake(It.IsAny<CustomerMakeBookingRequest>()));
        }

        [Test]
        public void ReviewCreatesCorrectRequest()
        {
            CustomerMakeBookingRequest request = null;

            _bookingControllerFactory.BookingService
                .Setup(x => x.SummarizeCustomerMake(It.IsAny<CustomerMakeBookingRequest>()))
                .Callback(delegate(CustomerMakeBookingRequest x) { request = x; })
                .Returns(_booking.Object);

            _bookingControllerFactory.GetController().Review(_inViewModel);
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

        [Test]
        public void ReviewReturnsCorrectViewModel()
        { 
            var controller = _bookingControllerFactory.GetController();
            var viewResult = controller.Review(_inViewModel) as ViewResult;
            var outViewModel = viewResult.Model as ReviewViewModel;
            Assert.That(outViewModel.BusId, Is.EqualTo(_inViewModel.BusId));
        }
    }
}
