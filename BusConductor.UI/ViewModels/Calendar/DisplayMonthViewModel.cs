using System;

namespace BusConductor.UI.ViewModels.Calendar
{
    public class DisplayMonthViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public Guid BusId;
        public string MonthName { get; set; }
        public DisplayMonthWeekViewModel[] Weeks { get; set; }
        public int YearOfPreviousMonth { get; set; }
        public int MonthOfPreviousMonth { get; set; }
        public int YearOfNextMonth { get; set; }
        public int MonthOfNextMonth { get; set; }
    }
}