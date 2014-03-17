using System;
using BusConductor.Domain.Common;
using BusConductor.Domain.ParameterSets.PricingPeriod;

namespace BusConductor.Domain.Entities
{
    public class PricingPeriod : Entity<Guid>
    {
        private int _startMonth;
        private int _startDay;
        private int _endMonth;
        private int _endDay;
        private decimal _fridayToFridayRate;
        private decimal _fridayToMondayRate; //Automatically set this to be same as monday to friday
        private decimal _mondayToFridayRate;
        private Guid _busId;
        private Bus _bus;

        public virtual decimal FridayToFridayRate
        {
            get { return _fridayToFridayRate; }
            set { _fridayToFridayRate = value; }
        }

        public virtual decimal FridayToMondayRate
        {
            get { return _fridayToMondayRate; }
            set { _fridayToMondayRate = value; }
        }

        public virtual decimal MondayToFridayRate
        {
            get { return _mondayToFridayRate; }
            set { _mondayToFridayRate = value; }
        }

        public Guid BusId
        {
            get { return _busId; }
            set { _busId = value; }
        }

        public virtual Bus Bus
        {
            get { return _bus; }
            set { _bus = value; }
        }

        public virtual int StartMonth
        {
            get { return _startMonth; }
            set { _startMonth = value; }
        }

        public virtual int StartDay
        {
            get { return _startDay; }
            set { _startDay = value; }
        }

        public virtual int EndMonth
        {
            get { return _endMonth; }
            set { _endMonth = value; }
        }

        public virtual int EndDay
        {
            get { return _endDay; }
            set { _endDay = value; }
        }

        public virtual bool ContainsDate(DateTime date)
        {
            var sampleStartDate = new DateTime(2000, _startMonth, _startDay);
            var sampleEndDate = new DateTime(2000, _endMonth, _endDay);
            var sampleTestDate = new DateTime(2000, date.Month, date.Day);

            if (sampleStartDate > sampleEndDate)
            {
                sampleStartDate = new DateTime(1999, _startMonth, _startDay);

                if (sampleTestDate > sampleEndDate)
                {
                    sampleTestDate = new DateTime(1999, date.Month, date.Day);
                }
            }

            return sampleStartDate <= sampleTestDate
                && sampleEndDate >= sampleTestDate;
        }

        public static PricingPeriod Add(AddEditParameterSet parameterSet)
        {
            var pricingPeriod = new PricingPeriod();
            pricingPeriod.SetValues(parameterSet);
            pricingPeriod._createdOn = DateTime.Now;
            pricingPeriod._createdBy = parameterSet.CurrentUser;
            return pricingPeriod;
        }

        public virtual void Edit(AddEditParameterSet parameterSet)
        {
            SetValues(parameterSet);
            _lastModifiedOn = DateTime.Now;
            _lastModifiedBy = parameterSet.CurrentUser;
        }

        protected virtual void SetValues(AddEditParameterSet parameterSet)
        {
            _id = parameterSet.Id;
            _bus = parameterSet.Bus;
            _startMonth = parameterSet.StartMonth;
            _startDay = parameterSet.StartDay;
            _endMonth = parameterSet.EndMonth;
            _endDay = parameterSet.EndDay;
            _fridayToFridayRate = parameterSet.FridayToFridayRate;
            _fridayToMondayRate = parameterSet.FridayToMondayRate;
            _mondayToFridayRate = parameterSet.MondayToFridayRate;
        }
    }
}
