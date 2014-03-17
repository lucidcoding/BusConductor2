using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusConductor.Domain.RepositoryContracts;
using BusConductor.UI.ActionFilters;
using BusConductor.UI.ViewModels.Rates;
using BusConductor.UI.ViewModelMappers.Rates;

namespace BusConductor.UI.Controllers
{
    public class RatesController : Controller
    {
        private readonly IBusRepository _busRepository;

        public RatesController(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        [EntityFrameworkReadContext]
        public ActionResult Index()
        {
            var busses = _busRepository.GetAll();
            var viewModel = new IndexViewModel();
            viewModel.Busses = new List<IndexBusViewModel>();

            foreach(var bus in busses)
            {
                var busViewModel = new IndexBusViewModel();
                busViewModel.BusId = bus.Id.Value;
                busViewModel.Name = bus.Name;
                busViewModel.MainImageUrl = VirtualPathUtility.ToAbsolute("~/Images/bluebell_sm_121109.jpg");
                busViewModel.Table = TableViewModelMapper.Map(bus);
                viewModel.Busses.Add(busViewModel);
            }

            return View(viewModel);
        }
    }
}
