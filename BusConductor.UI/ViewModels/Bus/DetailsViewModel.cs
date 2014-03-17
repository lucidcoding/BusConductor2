using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using BusConductor.UI.ViewModels.Calendar;
using BusConductor.UI.ViewModels.Rates;

namespace BusConductor.UI.ViewModels.Bus
{
    public class DetailsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Overview { get; set; }
        public string Details { get; set; }
        public DisplayMonthViewModel Calendar { get; set; }
        public TableViewModel RatesTable { get; set; }
    }
}