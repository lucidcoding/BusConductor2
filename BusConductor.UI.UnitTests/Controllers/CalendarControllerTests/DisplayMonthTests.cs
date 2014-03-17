using System;
using System.Linq;
using BusConductor.UI.UnitTests.ControllerFactories;
using BusConductor.UI.ViewModels.Calendar;
using NUnit.Framework;

namespace BusConductor.UI.UnitTests.Controllers.CalendarControllerTests
{
    [TestFixture]
    public class DisplayMonthTests
    {
        [Test]
        public void ReturnsCorrectViewModelForNovember2013()
        {
            var calendarControllerFactory = new CalendarControllerFactory(2013, 11, 11, 15, 15, 18);
            var calendarController = calendarControllerFactory.GetController();

            var result = calendarController.DisplayMonth(
                calendarControllerFactory.Year,
                calendarControllerFactory.Month,
                calendarControllerFactory.BusId);

            var viewModel = result.Model as DisplayMonthViewModel;
            Assert.That(viewModel.MonthName, Is.EqualTo("November"));
            Assert.That(viewModel.Weeks.Count(), Is.EqualTo(5));
            Assert.That(viewModel.Weeks[0].Days[0].Date, Is.EqualTo(new DateTime(2013, 10, 28)));
            Assert.That(viewModel.Weeks[0].Days[4].Date, Is.EqualTo(new DateTime(2013, 11, 1)));
            Assert.That(viewModel.Weeks[4].Days[5].Date, Is.EqualTo(new DateTime(2013, 11, 30)));
            Assert.That(viewModel.Weeks[4].Days[6].Date, Is.EqualTo(new DateTime(2013, 12, 1)));
            Assert.That(viewModel.Weeks[1].Days[6].AdditionalClass, Is.Null);
            Assert.That(viewModel.Weeks[2].Days[0].AdditionalClass, Is.EqualTo("pending-pm"));
            Assert.That(viewModel.Weeks[2].Days[1].AdditionalClass, Is.EqualTo("pending"));
            Assert.That(viewModel.Weeks[2].Days[3].AdditionalClass, Is.EqualTo("pending"));
            Assert.That(viewModel.Weeks[2].Days[4].AdditionalClass, Is.EqualTo("pending-am-confirmed-pm"));
            Assert.That(viewModel.Weeks[2].Days[5].AdditionalClass, Is.EqualTo("confirmed"));
            Assert.That(viewModel.Weeks[2].Days[6].AdditionalClass, Is.EqualTo("confirmed"));
            Assert.That(viewModel.Weeks[3].Days[0].AdditionalClass, Is.EqualTo("confirmed-am"));
            Assert.That(viewModel.Weeks[3].Days[1].AdditionalClass, Is.Null);
        }

        [Test]
        public void ReturnsCorrectViewModelForDecember2013()
        {
            var calendarControllerFactory = new CalendarControllerFactory(2013, 12, 13, 20, 23, 27);
            var calendarController = calendarControllerFactory.GetController();

            var result = calendarController.DisplayMonth(
                calendarControllerFactory.Year,
                calendarControllerFactory.Month,
                calendarControllerFactory.BusId);

            var viewModel = result.Model as DisplayMonthViewModel;
            Assert.That(viewModel.MonthName, Is.EqualTo("December"));
            Assert.That(viewModel.Weeks.Count(), Is.EqualTo(6));
            Assert.That(viewModel.Weeks[0].Days[0].Date, Is.EqualTo(new DateTime(2013, 11, 25)));
            Assert.That(viewModel.Weeks[0].Days[6].Date, Is.EqualTo(new DateTime(2013, 12, 1)));
            Assert.That(viewModel.Weeks[5].Days[1].Date, Is.EqualTo(new DateTime(2013, 12, 31)));
            Assert.That(viewModel.Weeks[5].Days[6].Date, Is.EqualTo(new DateTime(2014, 1, 5)));
            Assert.That(viewModel.Weeks[2].Days[3].AdditionalClass, Is.Null);
            Assert.That(viewModel.Weeks[2].Days[4].AdditionalClass, Is.EqualTo("pending-pm"));
            Assert.That(viewModel.Weeks[2].Days[5].AdditionalClass, Is.EqualTo("pending"));
            Assert.That(viewModel.Weeks[3].Days[3].AdditionalClass, Is.EqualTo("pending"));
            Assert.That(viewModel.Weeks[3].Days[4].AdditionalClass, Is.EqualTo("pending-am"));
            Assert.That(viewModel.Weeks[3].Days[5].AdditionalClass, Is.Null);
            Assert.That(viewModel.Weeks[3].Days[6].AdditionalClass, Is.Null);
            Assert.That(viewModel.Weeks[4].Days[0].AdditionalClass, Is.EqualTo("confirmed-pm"));
            Assert.That(viewModel.Weeks[4].Days[1].AdditionalClass, Is.EqualTo("confirmed"));
            Assert.That(viewModel.Weeks[4].Days[3].AdditionalClass, Is.EqualTo("confirmed"));
            Assert.That(viewModel.Weeks[4].Days[4].AdditionalClass, Is.EqualTo("confirmed-am"));
            Assert.That(viewModel.Weeks[4].Days[5].AdditionalClass, Is.Null);
        }

        [Test]
        public void ReturnsCorrectViewModelForMarch2014()
        {
            var calendarControllerFactory = new CalendarControllerFactory(2014, 3, 10, 14, 17, 21);
            var calendarController = calendarControllerFactory.GetController();

            var result = calendarController.DisplayMonth(
                calendarControllerFactory.Year,
                calendarControllerFactory.Month,
                calendarControllerFactory.BusId);

            var viewModel = result.Model as DisplayMonthViewModel;
            Assert.That(viewModel.MonthName, Is.EqualTo("March"));
            Assert.That(viewModel.Weeks.Count(), Is.EqualTo(6));
            Assert.That(viewModel.Weeks[0].Days[0].Date, Is.EqualTo(new DateTime(2014, 2, 24)));
            Assert.That(viewModel.Weeks[0].Days[6].Date, Is.EqualTo(new DateTime(2014, 3, 2)));
            Assert.That(viewModel.Weeks[5].Days[0].Date, Is.EqualTo(new DateTime(2014, 3, 31)));
            Assert.That(viewModel.Weeks[5].Days[6].Date, Is.EqualTo(new DateTime(2014, 4, 6)));
            Assert.That(viewModel.Weeks[1].Days[6].AdditionalClass, Is.Null);
            Assert.That(viewModel.Weeks[2].Days[0].AdditionalClass, Is.EqualTo("pending-pm"));
            Assert.That(viewModel.Weeks[2].Days[1].AdditionalClass, Is.EqualTo("pending"));
            Assert.That(viewModel.Weeks[2].Days[3].AdditionalClass, Is.EqualTo("pending"));
            Assert.That(viewModel.Weeks[2].Days[4].AdditionalClass, Is.EqualTo("pending-am"));
            Assert.That(viewModel.Weeks[2].Days[5].AdditionalClass, Is.Null);
        }
    }
}
