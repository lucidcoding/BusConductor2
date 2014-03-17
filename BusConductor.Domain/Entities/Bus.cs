using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using BusConductor.Domain.Common;
using BusConductor.Domain.Enumerations;
using BusConductor.Domain.ParameterSets.Bus;
using BusConductor.Domain.ParameterSets.PricingPeriod;

namespace BusConductor.Domain.Entities
{
    public class Bus : Entity<Guid>
    {
        private string _name;
        private string _description;
        private string _overview;
        private string _details;
        private DriveSide _driveSide;
        private int _berth;
        private int _year;
        private ICollection<PricingPeriod> _pricingPeriods; 
        private ICollection<Booking> _bookings;
 
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public virtual ICollection<Booking> Bookings
        {
            get { return _bookings; }
            set { _bookings = value; }
        }

        public virtual string Overview
        {
            get { return _overview; }
            set { _overview = value; }
        }

        public virtual string Details
        {
            get { return _details; }
            set { _details = value; }
        }

        public virtual DriveSide DriveSide
        {
            get { return _driveSide; }
            set { _driveSide = value; }
        }

        public virtual int Berth
        {
            get { return _berth; }
            set { _berth = value; }
        }

        public virtual int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public virtual ICollection<PricingPeriod> PricingPeriods
        {
            get { return _pricingPeriods; }
            set { _pricingPeriods = value; }
        }

        public virtual decimal GetRateFor(DateTime date)
        {
            return 0;
        }

        public virtual ValidationMessageCollection ValidateEdit(EditParameterSet parameterSet)
        {
            var validationMessages = new ValidationMessageCollection();
            bool overlappingPricingPeriods = false;
            bool daysUnmatchedByPricingPeriods = false;

            var newPricingPeriods = parameterSet.PricingPeriods.Select(pricingPeriod => PricingPeriod.Add(new AddEditParameterSet
            {
                StartMonth = pricingPeriod.StartMonth,
                StartDay = pricingPeriod.StartDay,
                EndMonth = pricingPeriod.EndMonth,
                EndDay = pricingPeriod.EndDay
            }));

            for (var date = new DateTime(2000, 1, 1); date < new DateTime(2001, 1, 1); date = date.AddDays(1))
            {
                var matchingPeriods = newPricingPeriods.Count(pricingPeriod => pricingPeriod.ContainsDate(date));

                if(matchingPeriods == 0)
                {
                    daysUnmatchedByPricingPeriods = true;
                }

                if(matchingPeriods > 1)
                {
                    overlappingPricingPeriods = true;
                }
            }

            if(daysUnmatchedByPricingPeriods)
            {
                validationMessages.AddError("Some days are not covered by pricing periods.");
            }

            if(overlappingPricingPeriods)
            {
                validationMessages.AddError("Some days are covered by more than one pricing period");
            }

            var firstPricingPeriod = newPricingPeriods
                .OrderBy(x => x.StartMonth)
                .ThenBy(x => x.StartDay)
                .First();

            var lastPricingPeriod = newPricingPeriods
                .OrderBy(x => x.StartMonth)
                .ThenBy(x => x.StartDay)
                .Last();

            if(firstPricingPeriod.StartMonth != 1 ||firstPricingPeriod.StartDay != 1)
            {
                validationMessages.AddError("There must be a pricing period beginning January 1st");
            }

            if (lastPricingPeriod.EndMonth != 12 || lastPricingPeriod.EndDay != 31)
            {
                validationMessages.AddError("There must be a pricing period ending December 31st");
            }

            return validationMessages;
        }

        public virtual void Edit(EditParameterSet parameterSet)
        {
            var now = DateTime.Now;
            _name = parameterSet.Name;
            _description = parameterSet.Description;
            _overview = parameterSet.Overview;
            _details = parameterSet.Details;
            _driveSide = parameterSet.DriveSide;
            _berth = parameterSet.Berth;
            _year = parameterSet.Year;
            _lastModifiedBy = parameterSet.CurrentUser;
            _lastModifiedOn = now;

            foreach(var editPricingPeriodParameterSet in parameterSet.PricingPeriods)
            {
                var addEditParameterSet = new AddEditParameterSet();
                addEditParameterSet.Id = editPricingPeriodParameterSet.Id;
                addEditParameterSet.Bus = this;
                addEditParameterSet.StartMonth = editPricingPeriodParameterSet.StartMonth;
                addEditParameterSet.StartDay = editPricingPeriodParameterSet.StartDay;
                addEditParameterSet.EndMonth = editPricingPeriodParameterSet.EndMonth;
                addEditParameterSet.EndDay = editPricingPeriodParameterSet.EndDay;
                addEditParameterSet.FridayToFridayRate = editPricingPeriodParameterSet.FridayToFridayRate;
                addEditParameterSet.FridayToMondayRate = editPricingPeriodParameterSet.FridayToMondayRate;
                addEditParameterSet.MondayToFridayRate = editPricingPeriodParameterSet.MondayToFridayRate;
                addEditParameterSet.CurrentUser = editPricingPeriodParameterSet.CurrentUser;
                
                if(_pricingPeriods.Any(x => x.Id.Value == editPricingPeriodParameterSet.Id))
                {
                    var pricingPeriod = _pricingPeriods.Single(x => x.Id.Value == editPricingPeriodParameterSet.Id);
                    pricingPeriod.Edit(addEditParameterSet);
                }
                else
                {
                    var pricingPeriod = PricingPeriod.Add(addEditParameterSet);
                    _pricingPeriods.Add(pricingPeriod);
                }
            }
        }

        public virtual PricingPeriod GetPricingPeriodFor(DateTime pickUp)
        {
            return PricingPeriods.Single(x => x.ContainsDate(pickUp));
        }

        public virtual decimal GetUndiscountedRateFor(DateTime pickUp, DateTime dropOff)
        {
            var pricingPeriod = GetPricingPeriodFor(pickUp);

            if(pickUp.DayOfWeek == DayOfWeek.Monday && dropOff == pickUp.AddDays(4))
            {
                return pricingPeriod.MondayToFridayRate;
            }
            
            if (pickUp.DayOfWeek == DayOfWeek.Friday && dropOff == pickUp.AddDays(3))
            {
                return pricingPeriod.FridayToMondayRate;
            }
            
            if (pickUp.DayOfWeek == DayOfWeek.Friday && dropOff == pickUp.AddDays(7))
            {
                return pricingPeriod.FridayToFridayRate;
            }

            if (pickUp.DayOfWeek == DayOfWeek.Monday && dropOff > pickUp.AddDays(4))
            {
                return pricingPeriod.MondayToFridayRate + GetUndiscountedRateFor(pickUp.AddDays(4), dropOff);
            }

            if (pickUp.DayOfWeek == DayOfWeek.Friday && dropOff > pickUp.AddDays(7))
            {
                return pricingPeriod.FridayToFridayRate + GetUndiscountedRateFor(pickUp.AddDays(7), dropOff);
            }

            throw new Exception("Unexpected booking period: " 
                + pickUp.ToString(CultureInfo.InvariantCulture) 
                + " - " 
                + dropOff.ToString(CultureInfo.InvariantCulture));
        }

        public virtual BusDayBookingStatus GetBookingStatusFor(DateTime date)
        {
            var returnValue = BusDayBookingStatus.Free;

            foreach (var booking in _bookings) 
            {
                if (date > booking.PickUp && date < booking.DropOff)
                {
                    return booking.Status == BookingStatus.Pending ? BusDayBookingStatus.PendingAllDay : BusDayBookingStatus.ConfirmedAllDay;
                }

                if(date == booking.PickUp)
                {
                    returnValue |= booking.Status == BookingStatus.Pending ? BusDayBookingStatus.PendingPm : BusDayBookingStatus.ConfirmedPm;
                }

                if (date == booking.DropOff)
                {
                    returnValue |= booking.Status == BookingStatus.Pending ? BusDayBookingStatus.PendingAm : BusDayBookingStatus.ConfirmedAm;
                }
            }

            return returnValue;
        }

        public virtual IList<Booking> GetConflictingBookings(DateTime pickUp, DateTime dropOff)
        {
            return Bookings.Where(booking =>
                (pickUp >= booking.PickUp && pickUp < booking.DropOff) 
                || (dropOff > booking.PickUp && dropOff <= booking.DropOff)
                || (pickUp <= booking.PickUp && dropOff >= booking.DropOff))
                .ToList();
        }

        public virtual decimal GetWinterMondayToFridayRate()
        {
            return GetPricingPeriodFor(new DateTime(2001, 1, 1)).MondayToFridayRate;
        }

        public virtual decimal GetWinterFridayToMondayRate()
        {
            return GetPricingPeriodFor(new DateTime(2001, 1, 1)).FridayToMondayRate;
        }

        public virtual decimal GetWinterFridayToFridayRate()
        {
            return GetPricingPeriodFor(new DateTime(2001, 1, 1)).FridayToFridayRate;
        }

        public virtual decimal GetSpringMondayToFridayRate()
        {
            return GetPricingPeriodFor(new DateTime(2001, 4, 1)).MondayToFridayRate;
        }

        public virtual decimal GetSpringFridayToMondayRate()
        {
            return GetPricingPeriodFor(new DateTime(2001, 4, 1)).FridayToMondayRate;
        }

        public virtual decimal GetSpringFridayToFridayRate()
        {
            return GetPricingPeriodFor(new DateTime(2001, 4, 1)).FridayToFridayRate;
        }

        public virtual decimal GetSummerMondayToFridayRate()
        {
            return GetPricingPeriodFor(new DateTime(2001, 7, 1)).MondayToFridayRate;
        }

        public virtual decimal GetSummerFridayToMondayRate()
        {
            return GetPricingPeriodFor(new DateTime(2001, 7, 1)).FridayToMondayRate;
        }

        public virtual decimal GetSummerFridayToFridayRate()
        {
            return GetPricingPeriodFor(new DateTime(2001, 7, 1)).FridayToFridayRate;
        }

        public virtual decimal GetAutumnMondayToFridayRate()
        {
            return GetPricingPeriodFor(new DateTime(2001, 10, 1)).MondayToFridayRate;
        }

        public virtual decimal GetAutumnFridayToMondayRate()
        {
            return GetPricingPeriodFor(new DateTime(2001, 10, 1)).FridayToMondayRate;
        }

        public virtual decimal GetAutumnFridayToFridayRate()
        {
            return GetPricingPeriodFor(new DateTime(2001, 10, 1)).FridayToFridayRate;
        }
    }
}
