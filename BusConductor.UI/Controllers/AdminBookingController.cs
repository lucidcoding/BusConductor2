using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusConductor.Application.Contracts;

namespace BusConductor.UI.Controllers
{
    public class AdminBookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public AdminBookingController(
            IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public ActionResult Index()
        { 
            return View();
        }

    }
}
