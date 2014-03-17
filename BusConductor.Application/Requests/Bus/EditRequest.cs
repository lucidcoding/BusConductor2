using System;
using System.Collections.Generic;
using BusConductor.Domain.Enumerations;

namespace BusConductor.Application.Requests.Bus
{
    public class EditRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Overview { get; set; }
        public string Details { get; set; }
        public DriveSide DriveSide { get; set; }
        public int Berth { get; set; }
        public int Year { get; set; }
        public IList<EditPricingPeriodRequest> PricingPeriods { get; set; }
    }
}
