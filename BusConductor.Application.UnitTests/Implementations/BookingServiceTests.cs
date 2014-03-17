using BusConductor.Application.Requests.Booking;
using BusConductor.Application.UnitTests.ServiceFactories;
using BusConductor.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusConductor.Application.UnitTests.Implementations
{
    [TestFixture]
    public class ValidateMakePendingTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void ValidateCustomerMakeCallsCorrectMethods()
        {
            var bookingServiceFactory = new BookingServiceFactory();
            var bookingService = bookingServiceFactory.GetService();
            var request = new CustomerMakeBookingRequest();
            var booking = bookingService.ValidateCustomerMake(request);
            bookingServiceFactory.CustomerMakeParameterSetMapper.Verify(x => x.Map(request), Times.Once());
        }

        [Test]
        public void SummarizeCustomerMakeCallsCorrectMethods()
        {
            var bookingServiceFactory = new BookingServiceFactory();
            var bookingService = bookingServiceFactory.GetService();
            var request = new CustomerMakeBookingRequest();
            var booking = bookingService.SummarizeCustomerMake(request);
            bookingServiceFactory.CustomerMakeParameterSetMapper.Verify(x => x.Map(request), Times.Once());
        }

        [Test]
        public void CustomerMakeCallsCorrectMethods()
        {
            var bookingServiceFactory = new BookingServiceFactory();
            var bookingService = bookingServiceFactory.GetService();
            var request = new CustomerMakeBookingRequest();
            bookingService.CustomerMake(request);
            bookingServiceFactory.CustomerMakeParameterSetMapper.Verify(x => x.MapWithOtherBookingsToday(request), Times.Once());
            bookingServiceFactory.BookingRepository.Verify(x => x.Save(It.IsAny<Booking>()), Times.Once());
        }

        [Test]
        public void CustomerMakeReturnsCorrectResult()
        {
            var bookingServiceFactory = new BookingServiceFactory();
            var bookingService = bookingServiceFactory.GetService();
            var request = new CustomerMakeBookingRequest();
            var bookingNumber = bookingService.CustomerMake(request);
            Assert.That(bookingNumber, Is.EqualTo("201310010003_Blue"));
        }

        [Test]
        public void ValidateAdminMakeCallsCorrectMethods()
        {
            var bookingServiceFactory = new BookingServiceFactory();
            var bookingService = bookingServiceFactory.GetService();
            var request = new AdminMakeBookingRequest();
            var booking = bookingService.ValidateAdminMake(request);
            bookingServiceFactory.AdminMakeParameterSetMapper.Verify(x => x.Map(request), Times.Once());
        }

        [Test]
        public void SummarizeAdminMakeCallsCorrectMethods()
        {
            var bookingServiceFactory = new BookingServiceFactory();
            var bookingService = bookingServiceFactory.GetService();
            var request = new AdminMakeBookingRequest();
            var booking = bookingService.SummarizeAdminMake(request);
            bookingServiceFactory.AdminMakeParameterSetMapper.Verify(x => x.Map(request), Times.Once());
        }

        [Test]
        public void AdminMakeCallsCorrectMethods()
        {
            var bookingServiceFactory = new BookingServiceFactory();
            var bookingService = bookingServiceFactory.GetService();
            var request = new AdminMakeBookingRequest();
            bookingService.AdminMake(request);
            bookingServiceFactory.AdminMakeParameterSetMapper.Verify(x => x.MapWithOtherBookingsToday(request), Times.Once());
            bookingServiceFactory.BookingRepository.Verify(x => x.Save(It.IsAny<Booking>()), Times.Once());
        }

        [Test]
        public void AdminMakeReturnsCorrectResult()
        {
            var bookingServiceFactory = new BookingServiceFactory();
            var bookingService = bookingServiceFactory.GetService();
            var request = new AdminMakeBookingRequest();
            var bookingNumber = bookingService.AdminMake(request);
            Assert.That(bookingNumber, Is.EqualTo("201310020003_Green"));
        }
    }
}
