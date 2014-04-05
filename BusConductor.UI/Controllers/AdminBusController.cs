using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusConductor.Domain.RepositoryContracts;
using BusConductor.UI.ViewModelMappers.AdminBus;
using BusConductor.UI.ActionFilters;

namespace BusConductor.UI.Controllers
{
    public class AdminBusController : Controller
    {
        public IBusRepository _busRepository;

        public AdminBusController(
            IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        [EntityFrameworkReadContext]
        public ActionResult Index()
        {
            var busses = _busRepository.GetAll();
            var viewModel = IndexViewModelMapper.Map(busses);
            return View(viewModel);
        }

        [EntityFrameworkReadContext]
        public ActionResult Bookings(Guid busId)
        {
            var bus = _busRepository.GetById(busId);
            var viewModel = BookingsViewModelMapper.Map(bus);
            return View(viewModel);
        }

    }
}
