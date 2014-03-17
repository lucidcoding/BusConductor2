using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusConductor.Domain.Entities;
using BusConductor.Domain.Enumerations;
using BusConductor.Domain.RepositoryContracts;
using BusConductor.UI.ActionFilters;
using BusConductor.UI.ViewModelMappers.Calendar;
using BusConductor.UI.ViewModels.Bus;
using Lucidity.Utilities;
using BusConductor.UI.ViewModelMappers.Rates;

namespace BusConductor.UI.Controllers
{
    public class BusController : Controller
    {
        private readonly IBusRepository _busRepository;

        public BusController(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        [EntityFrameworkReadContext]
        public ActionResult Index()
        {
            var busses = _busRepository.GetAll();

            var viewModel = new IndexViewModel();
            viewModel.Busses = new List<IndexBusViewModel>();

            foreach (var bus in busses)
            {
                var busViewModel = PropertyMapper.MapMatchingProperties<Bus, IndexBusViewModel>(bus, true);
                busViewModel.MainImageUrl = VirtualPathUtility.ToAbsolute("~/Images/bluebell_sm_121109.jpg");
                viewModel.Busses.Add(busViewModel);
            }

            return View(viewModel);
        }

        [EntityFrameworkReadContext]
        public ActionResult Details(Guid id)
        {
            var bus = _busRepository.GetById(id);
            var viewModel = PropertyMapper.MapMatchingProperties<Bus, DetailsViewModel>(bus, true);
            viewModel.Calendar = DisplayMonthViewModelMapper.Map(bus);
            viewModel.RatesTable = TableViewModelMapper.Map(bus);
            return View(viewModel);
        }
    }
}
