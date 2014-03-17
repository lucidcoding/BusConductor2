using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using BusConductor.Application.Contracts;
using BusConductor.Domain.RepositoryContracts;
using BusConductor.UI.ActionFilters;
using BusConductor.UI.ViewModelMappers.Booking;
using BusConductor.UI.ViewModels.Booking;

namespace BusConductor.UI.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IBusRepository _busRepository;

        public BookingController(
            IBookingService bookingService,
            IBusRepository busRepository)
        {
            _bookingService = bookingService;
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
                viewModel.Busses.Add(busViewModel);
            }

            return View(viewModel);
        }

        [Log]
        [EntityFrameworkReadContext]
        public ActionResult Make(Guid busId)
        {
            var bus = _busRepository.GetById(busId);
            var viewModel = MakeViewModelMapper.Map(bus);
            return View(viewModel);
        }

        [Log]
        [HttpPost]
        [EntityFrameworkReadContext]
        public ActionResult Review(MakeViewModel inViewModel)
        {
            var request = MakeViewModelMapper.Map(inViewModel);
            var validationMessages = _bookingService.ValidateCustomerMake(request);
            validationMessages.ForEach(validationMessage => ModelState.AddModelError(validationMessage.Field, validationMessage.Text));
            
            if(!ModelState.IsValid)
            {
                var bus = _busRepository.GetById(inViewModel.BusId);
                MakeViewModelMapper.Hydrate(inViewModel, bus);
                return View("Make", inViewModel);
            }

            var booking = _bookingService.SummarizeCustomerMake(request);
            var outViewModel = ReviewViewModelMapper.Map(booking);
            return View(outViewModel);
        }

        [Log]
        [HttpPost]
        [TransactionScope]
        [EntityFrameworkWriteContext]
        public ActionResult Confirm(ReviewViewModel inViewModel)
        {
            var request = ReviewViewModelMapper.Map(inViewModel);
            var bookingNumber = _bookingService.CustomerMake(request);
            var outViewModel = new CompletedViewModel();
            outViewModel.BookingNumber = bookingNumber;
            return View("Completed", outViewModel);
        }
    }
}
