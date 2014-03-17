using System;
using System.Collections.Generic;
using System.Linq;
using BusConductor.Domain.Entities;
using BusConductor.UI.ViewModels.Rates;

namespace BusConductor.UI.ViewModelMappers.Rates
{
    public static class TableViewModelMapper
    {
        public static TableViewModel Map(Bus bus)
        {
            var table = new TableViewModel();
            table.PricingPeriods = new List<IndexBusPricingPeriodViewModel>();

            foreach (var pricingPeriod in bus.PricingPeriods.OrderBy(x => x.StartMonth).ThenBy(x => x.StartDay))
            {
                var pricingPeriodViewModel = new IndexBusPricingPeriodViewModel();
                pricingPeriodViewModel.PeriodName = new DateTime(2000, pricingPeriod.StartMonth, 1).ToString("MMMM");
                pricingPeriodViewModel.WeekRate = pricingPeriod.FridayToFridayRate;
                pricingPeriodViewModel.WeekendAndShortWeekRate = pricingPeriod.FridayToMondayRate;
                table.PricingPeriods.Add(pricingPeriodViewModel);
            }

            return table;
        }
    }
}