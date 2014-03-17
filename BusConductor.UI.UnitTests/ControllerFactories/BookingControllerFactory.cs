using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusConductor.Application.Contracts;
using BusConductor.Application.Requests.Booking;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.Enumerations;
using BusConductor.Domain.RepositoryContracts;
using BusConductor.UI.Controllers;
using Moq;

namespace BusConductor.UI.UnitTests.ControllerFactories
{
    public class BookingControllerFactory
    {
        public Mock<IBookingService> BookingService;
        public Mock<IBusRepository> BusRepository;
        public Guid BusId { get; set; }
        public Mock<Bus> Bus { get; set; }

        public BookingControllerFactory()
        {
            BookingService = new Mock<IBookingService>();
            BusRepository = new Mock<IBusRepository>();
            BusId = Guid.NewGuid();
            Bus = new Mock<Bus>();
            Bus.SetupGet(x => x.Id).Returns(BusId);

            BusRepository
                .Setup(x => x.GetById(BusId))
                .Returns(Bus.Object);

            BookingService
                .Setup(x => x.ValidateCustomerMake(It.IsAny<CustomerMakeBookingRequest>()))
                .Returns(new ValidationMessageCollection());
        }

        public BookingController GetController()
        {
            return new BookingController(BookingService.Object, BusRepository.Object);
        }
    }
}
