using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusConductor.UI.ViewModels.AdminBooking;
using BusConductor.Domain.Entities;
using Lucidity.Utilities;
using System.Globalization;

namespace BusConductor.UI.ViewModelMappers.AdminBooking
{
    public static class YearPlannerViewModelMapper
    {
        public static YearPlannerViewModel Map(IList<Bus> busses, int year)
        {
            var viewModel = new YearPlannerViewModel();
            viewModel.Months = new List<YearPlannerViewModelMonth>();

            for (int monthIndex = 1; monthIndex <= 12; monthIndex ++)
            {
                var viewModelMonth = new YearPlannerViewModelMonth();
                var firstDayOfMonth = new DateTime(year, monthIndex, 1);
                var firstDayOfFirstWeekOfMonth = DateHelper.GetFirstDayOfFirstPartWeekOfMonth(year, monthIndex);
                viewModelMonth.Name = firstDayOfMonth.ToString("MMMM", CultureInfo.InvariantCulture);
                viewModelMonth.Days = new List<YearPlannerViewModelDay>();

                for (var date = firstDayOfFirstWeekOfMonth; date < firstDayOfMonth.AddMonths(1); date = date.AddDays(1))
                {
                    var viewModelDay = new YearPlannerViewModelDay();
                    viewModelDay.Date = date;
                    viewModelMonth.Days.Add(viewModelDay);
                }

                viewModel.Months.Add(viewModelMonth);
            }

            return viewModel;
        }
    }
}