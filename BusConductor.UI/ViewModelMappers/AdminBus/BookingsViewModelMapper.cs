using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusConductor.UI.ViewModels.AdminBus;
using BusConductor.Domain.Entities;
using BusConductor.UI.ViewModels.Calendar;
using BusConductor.UI.ViewModelMappers.Calendar;

namespace BusConductor.UI.ViewModelMappers.AdminBus
{
    public static class BookingsViewModelMapper
    {
        public static BookingsViewModel Map(Bus bus)
        {
            var viewModel = new BookingsViewModel();
            viewModel.BusId = bus.Id.Value;
            viewModel.BusName = bus.Name;
            var year = DateTime.Now.Year;
            viewModel.Months = new List<DisplayMonthViewModel>();

            for (int monthIndex = 1; monthIndex <= 12; monthIndex++)
            {
                var displayMonthViewModel = DisplayMonthViewModelMapper.Map(year, monthIndex, bus);
                viewModel.Months.Add(displayMonthViewModel);
            }

            return viewModel;
        }
    }
}