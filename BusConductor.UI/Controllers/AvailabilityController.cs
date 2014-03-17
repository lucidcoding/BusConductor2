using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusConductor.Domain.RepositoryContracts;
using BusConductor.UI.ActionFilters;
using BusConductor.UI.ViewModelMappers.Availability;
using BusConductor.UI.ViewModels.Availability;

namespace BusConductor.UI.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly IBusRepository _busRepository;

        public AvailabilityController(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        //todo: build query that is more efficient, only gettin grequired dates.
        [EntityFrameworkReadContext]
        public ActionResult Index()
        {
            var busses = _busRepository.GetAll();
            var viewModel = IndexViewModelMapper.Map(busses, DateTime.Today);
            return View(viewModel);
        }

        //todo: format dates like this? http://stackoverflow.com/questions/9160709/how-do-i-format-mvc-route-parameters
        [EntityFrameworkReadContext]
        public PartialViewResult DisplayFrom(int startYear, int startMonth, int startDay)
        {
            var busses = _busRepository.GetAll();
            var startDate = new DateTime(startYear, startMonth, startDay);
            var viewModel = IndexViewModelMapper.Map(busses, startDate);
            return PartialView("_IndexCalendar", viewModel);
        }
    }
}
