using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusConductor.UI.ViewModels.Calendar;

namespace BusConductor.UI.ViewModels.AdminBus
{
    public class BookingsViewModel
    {
        public Guid BusId { get; set; }
        public string BusName { get; set; }
        public IList<DisplayMonthViewModel> Months { get; set; }
    }
}