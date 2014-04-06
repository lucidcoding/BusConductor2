using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusConductor.UI.ViewModels.AdminBooking
{
    public class YearPlannerViewModel
    {
        public IList<YearPlannerViewModelMonth> Months { get; set; }
    }
}