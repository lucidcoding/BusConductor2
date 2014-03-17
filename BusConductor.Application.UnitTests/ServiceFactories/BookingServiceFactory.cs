using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BusConductor.Application.Contracts;
using BusConductor.Application.ParameterSetMappers.Booking;
using BusConductor.Application.Requests.Booking;
using BusConductor.Domain.Entities;
using BusConductor.Domain.Enumerations;
using BusConductor.Domain.ParameterSets;
using BusConductor.Domain.ParameterSets.Booking;
using BusConductor.Domain.RepositoryContracts;
using Moq;
using Lucidity.Utilities.Contracts.Logging;

namespace BusConductor.Application.UnitTests.ServiceFactories
{
    public class BookingServiceFactory
    {
        public Mock<IBookingRepository> BookingRepository { get; set; }
        public Mock<ICustomerMakeParameterSetMapper> CustomerMakeParameterSetMapper { get; set; }
        public Mock<IAdminMakeParameterSetMapper> AdminMakeParameterSetMapper { get; set; }
        public Mock<ILog> Log { get; set; }
        public Bus Bus { get; set; }
        public User ApplicationUser { get; set; }
        public User AdminUser { get; set; }
        public Role Role { get; set; }
        public Voucher Voucher { get; set; }
        public Booking Booking { get; set; }
        public CustomerMakeBookingParameterSet CustomerMakeBookingParameterSet { get; set; }
        public AdminMakeBookingParameterSet AdminMakeBookingParameterSet { get; set; }

        public BookingServiceFactory()
        {
            Log = new Mock<ILog>();
            BookingRepository = new Mock<IBookingRepository>();
            Bus = new Bus { Id = Guid.NewGuid(), Bookings = new List<Booking>(), PricingPeriods = new Collection<PricingPeriod>() };
            ApplicationUser = new User { Id = Guid.NewGuid() };
            AdminUser = new User { Id = Guid.NewGuid() };
            Role = new Role { Id = Guid.NewGuid() };
            Voucher = new Voucher { Id = Guid.NewGuid(), Code = "ABC123" };

            Bus.PricingPeriods.Add(new PricingPeriod
                                       {
                                           Bus = Bus,
                                           StartMonth = 1,
                                           StartDay = 1,
                                           EndMonth = 12,
                                           EndDay = 31,
                                           FridayToFridayRate = 1,
                                           FridayToMondayRate = 1,
                                           MondayToFridayRate = 1
                                       });

            CustomerMakeParameterSetMapper = new Mock<ICustomerMakeParameterSetMapper>();
            CustomerMakeBookingParameterSet = new CustomerMakeBookingParameterSet();
            CustomerMakeBookingParameterSet.PickUp = new DateTime(2090, 1, 2);
            CustomerMakeBookingParameterSet.DropOff = new DateTime(2090, 1, 6);
            CustomerMakeBookingParameterSet.Bus = Bus;
            CustomerMakeBookingParameterSet.Forename = "Barry";
            CustomerMakeBookingParameterSet.Surname = "Blue";
            CustomerMakeBookingParameterSet.AddressLine1 = "1 Orange Lane";
            CustomerMakeBookingParameterSet.AddressLine2 = "Address Line 2";
            CustomerMakeBookingParameterSet.AddressLine3 = "Address Line 3";
            CustomerMakeBookingParameterSet.Town = "Greenville";
            CustomerMakeBookingParameterSet.County = "Brownshire";
            CustomerMakeBookingParameterSet.PostCode = "M1 1AA";
            CustomerMakeBookingParameterSet.Email = "test@test.com";
            CustomerMakeBookingParameterSet.TelephoneNumber = "0123456789";
            CustomerMakeBookingParameterSet.IsMainDriver = true;
            CustomerMakeBookingParameterSet.DrivingLicenceNumber = "ABC1234";
            CustomerMakeBookingParameterSet.NumberOfAdults = 2;
            CustomerMakeBookingParameterSet.NumberOfChildren = 2;
            CustomerMakeBookingParameterSet.VoucherCode = Voucher.Code;
            CustomerMakeBookingParameterSet.Voucher = Voucher;
            CustomerMakeBookingParameterSet.RestrictionsAccepted = true;
            CustomerMakeBookingParameterSet.TermsAndConditionsAccepted = true;
            CustomerMakeBookingParameterSet.CurrentUser = ApplicationUser;
            CustomerMakeBookingParameterSet.CreatedOn = new DateTime(2013, 10, 1);

            CustomerMakeBookingParameterSet.OtherBookingsToday = new List<Booking>()
            {
                  new Booking{ BookingNumber = "201310010001_Black" },   
                  new Booking{ BookingNumber = "201310010002_Green" },                                 
            };

            CustomerMakeParameterSetMapper
                .Setup(x => x.Map(It.IsAny<CustomerMakeBookingRequest>()))
                .Returns(CustomerMakeBookingParameterSet);

            CustomerMakeParameterSetMapper
                .Setup(x => x.MapWithOtherBookingsToday(It.IsAny<CustomerMakeBookingRequest>()))
                .Returns(CustomerMakeBookingParameterSet);

            AdminMakeParameterSetMapper = new Mock<IAdminMakeParameterSetMapper>();
            AdminMakeBookingParameterSet = new AdminMakeBookingParameterSet();
            AdminMakeBookingParameterSet.PickUp = new DateTime(2090, 1, 6);
            AdminMakeBookingParameterSet.DropOff = new DateTime(2090, 1, 9);
            AdminMakeBookingParameterSet.Bus = Bus;
            AdminMakeBookingParameterSet.Forename = "Gary";
            AdminMakeBookingParameterSet.Surname = "Green";
            AdminMakeBookingParameterSet.AddressLine1 = "1 Black Lane";
            AdminMakeBookingParameterSet.AddressLine2 = "Address Line 2";
            AdminMakeBookingParameterSet.AddressLine3 = "Address Line 3";
            AdminMakeBookingParameterSet.Town = "Blueville";
            AdminMakeBookingParameterSet.County = "Purpleshire";
            AdminMakeBookingParameterSet.PostCode = "M1 1AA";
            AdminMakeBookingParameterSet.Email = "test2@test.com";
            AdminMakeBookingParameterSet.TelephoneNumber = "0987654321";
            AdminMakeBookingParameterSet.IsMainDriver = true;
            AdminMakeBookingParameterSet.DrivingLicenceNumber = "DEF5678";
            AdminMakeBookingParameterSet.NumberOfAdults = 1;
            AdminMakeBookingParameterSet.NumberOfChildren = 3;
            AdminMakeBookingParameterSet.CurrentUser = AdminUser;
            AdminMakeBookingParameterSet.CreatedOn = new DateTime(2013, 10, 2);
            AdminMakeBookingParameterSet.WarningsAcknowledged = true;
            AdminMakeBookingParameterSet.Status = BookingStatus.Confirmed;
            AdminMakeBookingParameterSet.TotalCost = 888m;

            AdminMakeBookingParameterSet.OtherBookingsToday = new List<Booking>()
            {
                  new Booking{ BookingNumber = "201310010001_Black" },   
                  new Booking{ BookingNumber = "201310010002_Green" },                                 
            };

            AdminMakeParameterSetMapper
                .Setup(x => x.Map(It.IsAny<AdminMakeBookingRequest>()))
                .Returns(AdminMakeBookingParameterSet);

            AdminMakeParameterSetMapper
                .Setup(x => x.MapWithOtherBookingsToday(It.IsAny<AdminMakeBookingRequest>()))
                .Returns(AdminMakeBookingParameterSet);

        }

        public IBookingService GetService()
        {
            return new Application.Implementations.BookingService(
                BookingRepository.Object,
                CustomerMakeParameterSetMapper.Object,
                AdminMakeParameterSetMapper.Object,
                Log.Object);
        }
    }
}
