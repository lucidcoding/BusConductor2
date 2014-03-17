using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusConductor.UI.ViewModels.Booking
{
    public class IndexViewModel
    {
        public IList<IndexBusViewModel> Busses { get; set; } 
    }
}