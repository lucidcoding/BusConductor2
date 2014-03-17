using System;
using BusConductor.Domain.Entities;
using BusConductor.Domain.Enumerations;
using BusConductor.Domain.RepositoryContracts;
using BusConductor.UI.Controllers;
using Moq;

namespace BusConductor.UI.UnitTests.ControllerFactories
{
    public class CalendarControllerFactory
    {
        public Mock<IBusRepository> BusRepository { get; set; }
        public Guid BusId { get; set; }
        public Mock<Bus> Bus { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public CalendarControllerFactory(
            int year, 
            int month, 
            int pendingBookingStartDate,
            int pendingBookingEndDate,
            int confirmedBookingStartDate,
            int confirmedBookingEndDate)
        {
            BusRepository = new Mock<IBusRepository>();
            BusId = Guid.NewGuid();
            Bus = new Mock<Bus>();
            Bus.SetupGet(x => x.Id).Returns(BusId);
            Bus.Setup(x => x.GetBookingStatusFor(It.IsAny<DateTime>())).Returns(BusDayBookingStatus.Free);

            for (var day = pendingBookingStartDate + 1; day < pendingBookingEndDate; day++)
            {
                Bus.Setup(x => x.GetBookingStatusFor(new DateTime(year, month, day))).Returns(BusDayBookingStatus.PendingAllDay);
            }

            Bus.Setup(x => x.GetBookingStatusFor(new DateTime(year, month, pendingBookingStartDate))).Returns(BusDayBookingStatus.PendingPm);
            Bus.Setup(x => x.GetBookingStatusFor(new DateTime(year, month, pendingBookingEndDate))).Returns(BusDayBookingStatus.PendingAm);

            for (var day = confirmedBookingStartDate + 1; day < confirmedBookingEndDate; day++)
            {
                Bus.Setup(x => x.GetBookingStatusFor(new DateTime(year, month, day))).Returns(BusDayBookingStatus.ConfirmedAllDay);
            }

            Bus.Setup(x => x.GetBookingStatusFor(new DateTime(year, month, confirmedBookingStartDate))).Returns(BusDayBookingStatus.ConfirmedPm);
            Bus.Setup(x => x.GetBookingStatusFor(new DateTime(year, month, confirmedBookingEndDate))).Returns(BusDayBookingStatus.ConfirmedAm);

            if(pendingBookingEndDate == confirmedBookingStartDate)
            {
                Bus.Setup(x => x.GetBookingStatusFor(new DateTime(year, month, confirmedBookingStartDate))).Returns(BusDayBookingStatus.PendingAm | BusDayBookingStatus.ConfirmedPm);
            }

            Year = year;
            Month = month;
            BusRepository.Setup(x => x.GetById(BusId)).Returns(Bus.Object);
        }

        public CalendarController GetController()
        {
            return new CalendarController(BusRepository.Object);
        }
    }
}
