using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusConductor.UI.ViewModels.AdminBooking
{
    public class IndexViewModel
    {
        public IList<IndexViewModelBooking> Bookings { get; set; }
    }
}