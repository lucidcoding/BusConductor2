using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using BusConductor.Domain.Entities;
using BusConductor.Domain.Enumerations;
using BusConductor.UI.ViewModels.Availability;
using System.Web.Mvc;

namespace BusConductor.UI.ViewModelMappers.Availability
{
    public static class IndexViewModelMapper
    {
        public static IndexViewModel Map(IList<Bus> busses, DateTime startDate)
        {
            const int numberOfDays = 25;
            var viewModel = new IndexViewModel();
            viewModel.Days = new List<IndexDayViewModel>();
            viewModel.Busses = new List<IndexBusViewModel>();
            viewModel.EarlierStart = startDate.AddDays(0 - numberOfDays);
            viewModel.LaterStart = startDate.AddDays(numberOfDays);

            for (int dayIndex = 0; dayIndex < numberOfDays; dayIndex++)
            {
                var day = startDate.AddDays(dayIndex);
                var dayViewModel = new IndexDayViewModel();
                dayViewModel.MonthName = day.ToString("MMM");
                dayViewModel.DayName = day.ToString("ddd");
                dayViewModel.DayNumber = day.Day;
                viewModel.Days.Add(dayViewModel);
            }

            for (int busIndex = 0; busIndex < busses.Count; busIndex++)
            {
                var busViewModel = new IndexBusViewModel();
                busViewModel.BusId = busses[busIndex].Id.Value;
                busViewModel.Name = busses[busIndex].Name;
                busViewModel.Days = new List<IndexBusDayViewModel>();

                //busViewModel.MainImageUrl = VirtualPathUtility.ToAbsolute("~/Images/bluebell_sm_121109.jpg");

                //Had to do it this way because of tests.
                if (HttpContext.Current != null)
                {
                    var httpContextBase = new HttpContextWrapper(HttpContext.Current);
                    busViewModel.MainImageUrl = UrlHelper.GenerateContentUrl("~/Images/bluebell_sm_121109.jpg",
                                                                             httpContextBase);
                }

                for (int dayIndex = 0; dayIndex < numberOfDays; dayIndex++)
                {
                    var busDayViewModel = new IndexBusDayViewModel();
                    var day = startDate.AddDays(dayIndex);

                    if(day.DayOfWeek == DayOfWeek.Friday || day.DayOfWeek == DayOfWeek.Monday)
                    {
                        busDayViewModel.AdditionalClass = "change-over-day";
                    }

                    if (busses[busIndex].GetBookingStatusFor(day) != BusDayBookingStatus.Free)
                    {
                        busDayViewModel.AdditionalClass = "booked";
                    }

                    busViewModel.Days.Add(busDayViewModel);
                }

                viewModel.Busses.Add(busViewModel);
            }

            return viewModel;
        }
    }
}