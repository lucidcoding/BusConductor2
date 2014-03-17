using System;

namespace BusConductor.Admin.UI.ViewModels.Bus
{
    public class EditPricingPeriodViewModel
    {
        public Guid Id { get; set; }
        public int StartMonth { get; set; }
        public int StartDay { get; set; }
        public int EndMonth { get; set; }
        public int EndDay { get; set; }
        public decimal FridayToFridayRate { get; set; }
        public decimal FridayToMondayRate { get; set; }
        public decimal MondayToFridayRate { get; set; }
    }
}
