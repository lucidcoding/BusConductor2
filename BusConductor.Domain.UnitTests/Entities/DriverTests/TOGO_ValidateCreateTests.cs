//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using BusConductor.Domain.Entities;
//using BusConductor.Domain.ParameterSets;
//using NUnit.Framework;

//namespace BusConductor.Domain.UnitTests.Entities.DriverTests
//{
//    [TestFixture]
//    public class ValidateCreateTests
//    {
//        private CreateDriverParameterSet _parameterSet;

//        [SetUp]
//        public void SetUp()
//        {
//            _parameterSet = new CreateDriverParameterSet();
//            _parameterSet.Id = Guid.NewGuid();
//            _parameterSet.Forename = "Arnold";
//            _parameterSet.Surname = "Amber";
//            _parameterSet.DrivingLicenceNumber = "ABCD12345";
//            _parameterSet.DateOfBirth = DateTime.Today.AddYears(-25);
//            _parameterSet.Booking = new Booking {Id = Guid.NewGuid()};
//            _parameterSet.IsMainDriver = true;
//            _parameterSet.ApplicationUser = new User {Id = Guid.NewGuid()};
//        }

//        [Test]
//        public void ValidParameterSetReturndNoMessages()
//        {
//            var validationMessages = Driver.ValidateCreate(_parameterSet);
//            Assert.That(validationMessages.Count, Is.EqualTo(0));
//        }

//        [Test]
//        public void MissingParametersReturnsErrors()
//        {
//            _parameterSet.Forename = null;
//            _parameterSet.Surname = null;
//            _parameterSet.DrivingLicenceNumber = null;
//            _parameterSet.DateOfBirth = null;
//            _parameterSet.Booking = null;
//            var validationMessages = Driver.ValidateCreate(_parameterSet);
//            Assert.That(validationMessages.Count, Is.EqualTo(5));
//            Assert.That(validationMessages.Any(x => x.Field == "Forename" && x.Text == "Forename is required."));
//            Assert.That(validationMessages.Any(x => x.Field == "Surname" && x.Text == "Surname is required."));
//            Assert.That(validationMessages.Any(x => x.Field == "DrivingLicenceNumber" && x.Text == "Driving licence number is required."));
//            Assert.That(validationMessages.Any(x => x.Field == "DateOfBirth" && x.Text == "Date of birth is required."));
//            Assert.That(validationMessages.Any(x => x.Field == "Booking" && x.Text == "Driver must refer to a booking."));
//        }

//        [Test]
//        public void TooYoungDriverReturnsError()
//        {
//            _parameterSet.DateOfBirth = _parameterSet.DateOfBirth.Value.AddDays(1);
//            var validationMessages = Driver.ValidateCreate(_parameterSet);
//            Assert.That(validationMessages.Count, Is.EqualTo(1));
//            Assert.That(validationMessages.Any(x => x.Field == "DateOfBirth" && x.Text == "Driver must be 25 or over."));
//        }
//    }
//}
