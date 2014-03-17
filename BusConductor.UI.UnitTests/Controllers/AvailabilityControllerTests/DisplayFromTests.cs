using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusConductor.UI.UnitTests.ControllerFactories;
using BusConductor.UI.ViewModelMappers.Availability;
using BusConductor.UI.ViewModels.Availability;
using NUnit.Framework;

namespace BusConductor.UI.UnitTests.Controllers.AvailabilityControllerTests
{
    [TestFixture]
    public class DisplayFromTests
    {
        private AvailabilityControllerFactory _availabilityControllerFactory;
        private IndexViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _availabilityControllerFactory = new AvailabilityControllerFactory();
        }

        [Test]
        public void ReturnsCorrectViewModel()
        {
            var result = _availabilityControllerFactory.GetController().DisplayFrom(
                _availabilityControllerFactory.StartDate.Year,
                _availabilityControllerFactory.StartDate.Month,
                _availabilityControllerFactory.StartDate.Day);

            var viewModel = result.Model as IndexViewModel;

            Assert.That(viewModel.Busses.Count, Is.EqualTo(2));
            Assert.That(viewModel.Busses[0].Name, Is.EqualTo("Bus 1"));
            Assert.That(viewModel.Busses[1].Name, Is.EqualTo("Bus 2"));

            for (int i = 0; i < 25; i++)
            {
                var date = _availabilityControllerFactory.StartDate.AddDays(i);

                if ((date.Day >= 2 && date.Day <= 6) || (date.Day >= 9 && date.Day <= 13))
                {
                    Assert.That(viewModel.Busses[0].Days[i].AdditionalClass, Is.EqualTo("booked"));
                }
                else
                {
                    if(date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Monday)
                    {
                        Assert.That(viewModel.Busses[0].Days[i].AdditionalClass, Is.EqualTo("change-over-day"));
                    }
                    else
                    {
                        Assert.That(viewModel.Busses[0].Days[i].AdditionalClass, Is.Null);
                    }
                }

                if (((date.Day >= 6 && date.Day <= 9) || (date.Day >= 13 && date.Day <= 16)))
                {
                    Assert.That(viewModel.Busses[1].Days[i].AdditionalClass, Is.EqualTo("booked"));
                }
                else
                {
                    if (date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Monday)
                    {
                        Assert.That(viewModel.Busses[1].Days[i].AdditionalClass, Is.EqualTo("change-over-day"));
                    }
                    else
                    {
                        Assert.That(viewModel.Busses[1].Days[i].AdditionalClass, Is.Null);
                    }
                }
            }
        }
    }
}
