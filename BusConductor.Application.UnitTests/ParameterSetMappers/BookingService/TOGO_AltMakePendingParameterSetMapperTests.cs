//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using BusConductor.Application.ParameterSetMappers.Booking;
//using BusConductor.Application.Requests.Booking;
//using BusConductor.Domain.Entities;
//using BusConductor.Domain.RepositoryContracts;
//using Moq;
//using NUnit.Framework;

//namespace BusConductor.Application.UnitTests.ParameterSetMappers.BookingService
//{
//    [TestFixture]
//    [Ignore]
//    public class TOGO_AltMakePendingParameterSetMapperTests
//    {
//        private Mock<IBusRepository> _busRepository;
//        private Mock<IUserRepository> _userRepository;
//        private Mock<IRoleRepository> _roleRepository;
//        private Mock<IVoucherRepository> _voucherRepository;
//        private Mock<Bus> _bus;
//        private Mock<User> _user;
//        private Mock<Role> _role;
//        private Mock<Voucher> _voucher;
//        private MakePendingParameterSetMapper _mapper;
//        private MakePendingRequest _request;

//        [SetUp]
//        public void SetUp()
//        {
//            _busRepository = new Mock<IBusRepository>();
//            _userRepository = new Mock<IUserRepository>();
//            _roleRepository = new Mock<IRoleRepository>();
//            _voucherRepository = new Mock<IVoucherRepository>();
//            _bus = new Mock<Bus>();
//            _user = new Mock<User>();
//            _role = new Mock<Role>();
//            _voucher = new Mock<Voucher>();

//            _bus.SetupGet(x => x.Id).Returns(Guid.NewGuid());
//            _user.SetupGet(x => x.Id).Returns(Guid.NewGuid());
//            _role.SetupGet(x => x.Id).Returns(Guid.NewGuid());
//            _voucher.SetupGet(x => x.Id).Returns(Guid.NewGuid());
//            _voucher.SetupGet(x => x.Code).Returns("ABC123");

//            var a = _bus.Object.Id;
//            var b = _bus.Object.Id;

//            _busRepository
//                .Setup(x => x.GetById(_bus.Object.Id.Value))
//                .Returns(_bus.Object);

//            _userRepository
//                .Setup(x => x.GetByUsername("Application"))
//                .Returns(_user.Object);

//            _roleRepository
//                .Setup(x => x.GetByName("Guest"))
//                .Returns(_role.Object);

//            _voucherRepository
//                .Setup(x => x.GetByCode(_voucher.Object.Code))
//                .Returns(_voucher.Object);

//            _mapper = new MakePendingParameterSetMapper(
//                _busRepository.Object,
//                _userRepository.Object,
//                _roleRepository.Object,
//                _voucherRepository.Object);

//            _request = new MakePendingRequest();
//            _request.BusId = _bus.Object.Id.Value;
//            _request.PickUp = new DateTime(2090, 10, 1);
//            _request.DropOff = new DateTime(2090, 10, 8);
//            _request.Forename = "Barry";
//            _request.Surname = "Blue";
//            _request.AddressLine1 = "1 Orange Lane";
//            _request.AddressLine2 = "Address Line 2";
//            _request.AddressLine3 = "Address Line 3";
//            _request.Town = "Greenville";
//            _request.County = "Brownshire";
//            _request.PostCode = "M1 1AA";
//            _request.Email = "test@test.com";
//            _request.TelephoneNumber = "0123456789";
//            _request.IsMainDriver = true;
//            _request.NumberOfAdults = 2;
//            _request.NumberOfChildren = 2;
//            _request.VoucherCode = _voucher.Object.Code;
//        }

//        [Test]
//        public void MappingIsCorrect()
//        {
//            var parameter = _mapper.Map(_request);
//            Assert.That(parameter.PickUp, Is.EqualTo(_request.PickUp));
//            Assert.That(parameter.DropOff, Is.EqualTo(_request.DropOff));
//            Assert.That(parameter.Bus, Is.EqualTo(_bus));
//            Assert.That(parameter.GuestRole, Is.EqualTo(_role));
//            Assert.That(parameter.Forename, Is.EqualTo(_request.Forename));
//            Assert.That(parameter.Surname, Is.EqualTo(_request.Surname));
//            Assert.That(parameter.AddressLine1, Is.EqualTo(_request.AddressLine1));
//            Assert.That(parameter.AddressLine2, Is.EqualTo(_request.AddressLine2));
//            Assert.That(parameter.AddressLine3, Is.EqualTo(_request.AddressLine3));
//            Assert.That(parameter.Town, Is.EqualTo(_request.Town));
//            Assert.That(parameter.County, Is.EqualTo(_request.County));
//            Assert.That(parameter.PostCode, Is.EqualTo(_request.PostCode));
//            Assert.That(parameter.Email, Is.EqualTo(_request.Email));
//            Assert.That(parameter.TelephoneNumber, Is.EqualTo(_request.TelephoneNumber));
//            Assert.That(parameter.IsMainDriver, Is.EqualTo(_request.IsMainDriver));
//            Assert.That(parameter.NumberOfAdults, Is.EqualTo(_request.NumberOfAdults));
//            Assert.That(parameter.NumberOfChildren, Is.EqualTo(_request.NumberOfChildren));
//            Assert.That(parameter.VoucherCode, Is.EqualTo(_request.VoucherCode));
//            Assert.That(parameter.Voucher, Is.EqualTo(_voucher));
//            Assert.That(parameter.CurrentUser, Is.EqualTo(_user));
//        }
//    }
//}
