using System;
using System.Collections.Generic;
using BusConductor.Domain.Entities;
using BusConductor.Domain.Enumerations;
using BusConductor.Domain.RepositoryContracts;
using BusConductor.UI.Controllers;
using Moq;

namespace BusConductor.UI.UnitTests.ControllerFactories
{
    public class AvailabilityControllerFactory
    {
        public Mock<IBusRepository> BusRepository;
        public IList<Bus> Busses;
        public DateTime StartDate;

        public AvailabilityControllerFactory()
        {
            BusRepository = new Mock<IBusRepository>();
            StartDate = new DateTime(2090, 1, 1);//2, 6
            Busses = new List<Bus>();
            var bus1 = new Mock<Bus>();
            var bus1Id = Guid.NewGuid();
            bus1.SetupGet(x => x.Id).Returns(bus1Id);
            bus1.SetupGet(x => x.Name).Returns("Bus 1");

            for (int i = 0; i < 25; i++)
            {
                var date = StartDate.AddDays(i);

                if ((date.Day >= 2 && date.Day <= 6) || (date.Day >= 9 && date.Day <= 13))
                {
                    bus1.Setup(x => x.GetBookingStatusFor(date)).Returns(BusDayBookingStatus.PendingAllDay);
                }
                else
                {
                    bus1.Setup(x => x.GetBookingStatusFor(date)).Returns(BusDayBookingStatus.Free);
                }
            }

            Busses.Add(bus1.Object);
            var bus2 = new Mock<Bus>();
            var bus2Id = Guid.NewGuid();
            bus2.SetupGet(x => x.Id).Returns(bus2Id);
            bus2.SetupGet(x => x.Name).Returns("Bus 2");

            for (int i = 0; i < 25; i++)
            {
                var date = StartDate.AddDays(i);

                if ((date.Day >= 6 && date.Day <= 9) || (date.Day >= 13 && date.Day <= 16))
                {
                    bus2.Setup(x => x.GetBookingStatusFor(date)).Returns(BusDayBookingStatus.PendingAllDay);
                }
                else
                {
                    bus2.Setup(x => x.GetBookingStatusFor(date)).Returns(BusDayBookingStatus.Free);
                }
            }

            Busses.Add(bus2.Object);

            BusRepository
                .Setup(x => x.GetAll())
                .Returns(Busses);
        }

        public AvailabilityController GetController()
        {
            return new AvailabilityController(BusRepository.Object);
        }
    }
}
