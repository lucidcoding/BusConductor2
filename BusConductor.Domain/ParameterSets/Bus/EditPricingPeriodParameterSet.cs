using System;
using BusConductor.Domain.Entities;

namespace BusConductor.Domain.ParameterSets.Bus
{
    public class EditPricingPeriodParameterSet
    {
        public Guid Id { get; set; }
        public int StartMonth { get; set; }
        public int StartDay { get; set; }
        public int EndMonth { get; set; }
        public int EndDay { get; set; }
        public decimal FridayToFridayRate { get; set; }
        public decimal FridayToMondayRate { get; set; }
        public decimal MondayToFridayRate { get; set; }
        public User CurrentUser { get; set; }
    }
}
