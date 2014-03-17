using System;
using System.Collections.ObjectModel;
using BusConductor.Domain.Enumerations;

namespace BusConductor.Admin.UI.ViewModels.Bus
{
    public class EditViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Overview { get; set; }
        public string Details { get; set; }
        public DriveSide DriveSide { get; set; }
        public int Berth { get; set; }
        public int Year { get; set; }
        public ObservableCollection<EditPricingPeriodViewModel> PricingPeriods { get; set; }
    }
}
