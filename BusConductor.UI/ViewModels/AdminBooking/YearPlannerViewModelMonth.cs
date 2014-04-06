using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusConductor.UI.ViewModels.AdminBooking
{
    public class YearPlannerViewModelMonth
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public IList<YearPlannerViewModelDay> Days { get; set; }
    }
}