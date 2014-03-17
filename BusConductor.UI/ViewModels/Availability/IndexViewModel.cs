using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusConductor.UI.ViewModels.Availability
{
    public class IndexViewModel
    {
        public IList<IndexDayViewModel> Days { get; set; }
        public IList<IndexBusViewModel> Busses { get; set; }
        public DateTime EarlierStart { get; set; }
        public DateTime LaterStart { get; set; }
    }
}