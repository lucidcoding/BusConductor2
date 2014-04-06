using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusConductor.Application.Contracts;
using BusConductor.UI.ViewModels.AdminBooking;
using BusConductor.UI.ActionFilters;
using BusConductor.UI.ViewModelMappers.AdminBooking;
using BusConductor.Domain.RepositoryContracts;
using BusConductor.Domain.Enumerations;

namespace BusConductor.UI.Controllers
{
    public class AdminBookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IBusRepository _busRepository;

        public AdminBookingController(
            IBookingService bookingService,
            IBusRepository busRepository)
        {
            _bookingService = bookingService;
            _busRepository = busRepository;
        }

        [EntityFrameworkReadContext]
        public ActionResult Index()
        { 
            return View();
        }

        [EntityFrameworkReadContext]
        public ActionResult YearPlanner()
        {
            var busses = _busRepository.GetAll();
            var viewModel = YearPlannerViewModelMapper.Map(busses, 2014);
            return View(viewModel);
        }

        [EntityFrameworkReadContext]
        public ActionResult Make(Guid busId)
        {
            var viewModel = new MakeViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [TransactionScope]
        [EntityFrameworkWriteContext]
        public ActionResult Make(MakeViewModel viewModel)
        {
            viewModel.Status = BookingStatus.Confirmed;

            var request = MakeViewModelMapper.Map(viewModel);
            var validationMessages = _bookingService.ValidateAdminMake(request);
            validationMessages.ForEach(validationMessage => ModelState.AddModelError(validationMessage.Field, validationMessage.Text));

            if (!ModelState.IsValid)
            {
                //var bus = _busRepository.GetById(viewModel.BusId);
                //MakeViewModelMapper.Hydrate(inViewModel, bus);
                return View("Make", viewModel);
            }

            var booking = _bookingService.AdminMake(request);

            //var booking = _bookingService.SummarizeCustomerMake(request);
            //var outViewModel = ReviewViewModelMapper.Map(booking);
            //return View(outViewModel);

            return RedirectToAction("Index", "AdminBus", new { busId = viewModel.BusId });
        }
    }
}
