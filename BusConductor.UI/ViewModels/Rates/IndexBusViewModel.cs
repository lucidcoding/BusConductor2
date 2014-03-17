using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusConductor.UI.ViewModels.Rates
{
    public class IndexBusViewModel
    {
        public Guid BusId { get; set; }
        public string Name { get; set; }
        public string MainImageUrl { get; set; }
        public TableViewModel Table { get; set; }
    }
}