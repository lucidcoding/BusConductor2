using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusConductor.Domain.RepositoryContracts;
using BusConductor.UI.ActionFilters;
using BusConductor.UI.ViewModelMappers.Calendar;
using BusConductor.UI.ViewModels.Bus;

namespace BusConductor.UI.Controllers
{
    public class CalendarController : Controller
    {
        private readonly IBusRepository _busRepository;

        public CalendarController(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        [EntityFrameworkReadContext]
        public PartialViewResult DisplayMonth(int year, int month, Guid busId)
        {
            var bus = _busRepository.GetById(busId);
            var viewModel = DisplayMonthViewModelMapper.Map(year, month, bus);
            return PartialView("_Calendar", viewModel);
        }
    }
}
