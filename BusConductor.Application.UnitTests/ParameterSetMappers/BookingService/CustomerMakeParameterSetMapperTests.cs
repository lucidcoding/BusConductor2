﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusConductor.Application.ParameterSetMappers.Booking;
using BusConductor.Application.Requests.Booking;
using BusConductor.Domain.Entities;
using BusConductor.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusConductor.Application.UnitTests.ParameterSetMappers.BookingService
{
    [TestFixture]
    public class CustomerMakeParameterSetMapperTests
    {
        private Mock<IBusRepository> _busRepository;
        private Mock<IUserRepository> _userRepository;
        private Mock<IVoucherRepository> _voucherRepository;
        private Mock<IBookingRepository> _bookingRepository;
        private Bus _bus;
        private User _user;
        private Voucher _voucher;
        private CustomerMakeParameterSetMapper _mapper;
        private CustomerMakeBookingRequest _request;

        [SetUp]
        public void SetUp()
        {
            _busRepository = new Mock<IBusRepository>();
            _userRepository = new Mock<IUserRepository>();
            _voucherRepository = new Mock<IVoucherRepository>();
            _bookingRepository = new Mock<IBookingRepository>();
            _bus = new Bus { Id = Guid.NewGuid() };
            _user = new User { Id = Guid.NewGuid() };
            _voucher = new Voucher { Id = Guid.NewGuid(), Code = "ABC123" };

            _busRepository
                .Setup(x => x.GetById(_bus.Id.Value))
                .Returns(_bus);

            _userRepository
                .Setup(x => x.GetByUsername("Application"))
                .Returns(_user);

            _voucherRepository
                .Setup(x => x.GetByCode(_voucher.Code))
                .Returns(_voucher);

            _bookingRepository
                .Setup(x => x.GetByDate(It.IsAny<DateTime>()))
                .Returns(new List<Booking>()
                             {
                                 new Booking {BookingNumber = "Book01"},
                                 new Booking {BookingNumber = "Book02"}
                             });

            _mapper = new CustomerMakeParameterSetMapper(
                _busRepository.Object,
                _userRepository.Object,
                _voucherRepository.Object,
                _bookingRepository.Object);

            _request = new CustomerMakeBookingRequest();
            _request.BusId = _bus.Id.Value;
            _request.PickUp = new DateTime(2090, 10, 1);
            _request.DropOff = new DateTime(2090, 10, 8);
            _request.Forename = "Barry";
            _request.Surname = "Blue";
            _request.AddressLine1 = "1 Orange Lane";
            _request.AddressLine2 = "Address Line 2";
            _request.AddressLine3 = "Address Line 3";
            _request.Town = "Greenville";
            _request.County = "Brownshire";
            _request.PostCode = "M1 1AA";
            _request.Email = "test@test.com";
            _request.TelephoneNumber = "0123456789";
            _request.IsMainDriver = true;
            _request.NumberOfAdults = 2;
            _request.NumberOfChildren = 2;
            _request.VoucherCode = _voucher.Code;
        }

        [Test]
        public void MappingIsCorrect()
        {
            var parameter = _mapper.MapWithOtherBookingsToday(_request);
            Assert.That(parameter.PickUp, Is.EqualTo(_request.PickUp));
            Assert.That(parameter.DropOff, Is.EqualTo(_request.DropOff));
            Assert.That(parameter.Bus, Is.EqualTo(_bus));
            Assert.That(parameter.Forename, Is.EqualTo(_request.Forename));
            Assert.That(parameter.Surname, Is.EqualTo(_request.Surname));
            Assert.That(parameter.AddressLine1, Is.EqualTo(_request.AddressLine1));
            Assert.That(parameter.AddressLine2, Is.EqualTo(_request.AddressLine2));
            Assert.That(parameter.AddressLine3, Is.EqualTo(_request.AddressLine3));
            Assert.That(parameter.Town, Is.EqualTo(_request.Town));
            Assert.That(parameter.County, Is.EqualTo(_request.County));
            Assert.That(parameter.PostCode, Is.EqualTo(_request.PostCode));
            Assert.That(parameter.Email, Is.EqualTo(_request.Email));
            Assert.That(parameter.TelephoneNumber, Is.EqualTo(_request.TelephoneNumber));
            Assert.That(parameter.IsMainDriver, Is.EqualTo(_request.IsMainDriver));
            Assert.That(parameter.NumberOfAdults, Is.EqualTo(_request.NumberOfAdults));
            Assert.That(parameter.NumberOfChildren, Is.EqualTo(_request.NumberOfChildren));
            Assert.That(parameter.VoucherCode, Is.EqualTo(_request.VoucherCode));
            Assert.That(parameter.Voucher, Is.EqualTo(_voucher));
            Assert.That(parameter.CurrentUser, Is.EqualTo(_user));
            Assert.That(parameter.OtherBookingsToday.Count, Is.EqualTo(2));
            Assert.That(parameter.OtherBookingsToday.Any(x => x.BookingNumber == "Book01"));
            Assert.That(parameter.OtherBookingsToday.Any(x => x.BookingNumber == "Book02"));
        }
    }
}
