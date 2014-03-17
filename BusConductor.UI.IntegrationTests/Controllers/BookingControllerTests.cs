using System;
using System.Linq;
using BusConductor.Data.Common;
using BusConductor.Domain.RepositoryContracts;
using BusConductor.UI.Controllers;
using BusConductor.UI.IntegrationTests.Core;
using BusConductor.UI.IntegrationTests.Helpers;
using BusConductor.UI.ViewModels.Booking;
using NUnit.Framework;
using StructureMap;

namespace BusConductor.UI.IntegrationTests.Controllers
{
    [TestFixture]
    public class BookingControllerTests
    {
        private BookingController _bookingController;

        [SetUp]
        public void SetUp()
        {
            ObjectFactory.Container.Configure(x => x.AddRegistry<TestRegistry>());
            _bookingController = ObjectFactory.GetInstance<BookingController>();
            ScriptRunner.RunScript();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void Works()
        {
            var contextProvider = ObjectFactory.GetInstance<IContextProvider>();
            var viewModel = new ReviewViewModel();
            viewModel.BusId = new Guid("6a9857a6-d0b0-4e1a-84cb-ee9ade159560");
            viewModel.PickUp = new DateTime(2090, 1, 2);
            viewModel.DropOff = new DateTime(2090, 1, 6);
            viewModel.Forename = "Percy";
            viewModel.Surname = "Purple";
            viewModel.AddressLine1 = "5 Green Lane";
            viewModel.Town = "Blackville";
            viewModel.County = "Blueshire";
            viewModel.PostCode = "M1 1AA";
            viewModel.Email = "percy@purple.com";
            viewModel.TelephoneNumber = "percy@purple.com";
            viewModel.IsMainDriver = false;
            viewModel.DrivingLicenceNumber = "ABC1234";
            viewModel.DriverForename = "Betty";
            viewModel.DriverSurname = "Beige";
            viewModel.NumberOfAdults = 2;
            viewModel.NumberOfChildren = 1;
            viewModel.VoucherCode = null;
            viewModel.RestrictionsAccepted = true;
            viewModel.TermsAndConditionsAccepted = true;

            using (contextProvider)
            {
                _bookingController.Confirm(viewModel);
                contextProvider.SaveChanges();
            }

            using (contextProvider)
            {   
                var bookingRepository = ObjectFactory.GetInstance<IBookingRepository>();
                var booking = bookingRepository.GetAll().Single(x => x.Id.Value != new Guid("eaa01eab-f3bd-4e24-8368-d3501a227a8b"));
                Assert.That(booking.PickUp, Is.EqualTo(viewModel.PickUp));
                Assert.That(booking.DropOff, Is.EqualTo(viewModel.DropOff));
                Assert.That(booking.Bus.Id.Value, Is.EqualTo(viewModel.BusId));
                Assert.That(booking.Customer.Forename, Is.EqualTo(viewModel.Forename));
                Assert.That(booking.Customer.Surname, Is.EqualTo(viewModel.Surname));
                Assert.That(booking.Customer.AddressLine1, Is.EqualTo(viewModel.AddressLine1));
                Assert.That(booking.Customer.Town, Is.EqualTo(viewModel.Town));
                Assert.That(booking.Customer.County, Is.EqualTo(viewModel.County));
                Assert.That(booking.Customer.PostCode, Is.EqualTo(viewModel.PostCode));
                Assert.That(booking.Customer.TelephoneNumber, Is.EqualTo(viewModel.TelephoneNumber));
                Assert.That(booking.IsMainDriver, Is.EqualTo(viewModel.IsMainDriver));
                Assert.That(booking.DriverForename, Is.EqualTo(viewModel.DriverForename));
                Assert.That(booking.DriverSurname, Is.EqualTo(viewModel.DriverSurname));
                Assert.That(booking.NumberOfAdults, Is.EqualTo(viewModel.NumberOfAdults));
                Assert.That(booking.NumberOfChildren, Is.EqualTo(viewModel.NumberOfChildren));
                Assert.That(booking.TotalCost, Is.EqualTo(600));
                Assert.That(booking.CreatedBy.Id.Value, Is.EqualTo(new Guid("c8238876-47fc-42af-8a32-926061097f1c")));
                Assert.That(booking.CreatedBy.Username, Is.EqualTo("Application"));
            }
        }
    }
}
