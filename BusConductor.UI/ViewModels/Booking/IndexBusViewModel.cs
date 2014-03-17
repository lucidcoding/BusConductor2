using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusConductor.UI.ViewModels.Booking
{
    public class IndexBusViewModel
    {
        public Guid BusId { get; set; }
        public string Name { get; set; }
        public string MainImageUrl { get; set; }
    }
}