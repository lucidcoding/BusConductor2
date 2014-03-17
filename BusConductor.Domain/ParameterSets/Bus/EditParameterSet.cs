using System.Collections.Generic;
using BusConductor.Domain.Entities;
using BusConductor.Domain.Enumerations;

namespace BusConductor.Domain.ParameterSets.Bus
{
    public class EditParameterSet
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Overview { get; set; }
        public string Details { get; set; }
        public DriveSide DriveSide { get; set; }
        public int Berth { get; set; }
        public int Year { get; set; }
        public IList<EditPricingPeriodParameterSet> PricingPeriods { get; set; }
        public User CurrentUser { get; set; }
    }
}
