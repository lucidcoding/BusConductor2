using System;
using System.Linq;
using BusConductor.Domain.Common;
using BusConductor.Domain.Enumerations;
using BusConductor.Domain.ParameterSets;
using BusConductor.Domain.ParameterSets.Booking;
using BusConductor.Domain.ParameterSets.Customer;

namespace BusConductor.Domain.Entities
{
    public class Booking : Entity<Guid>
    {
        private string _bookingNumber;
        private Guid _customerId;
        private Customer _customer;
        private DateTime _pickUp;
        private DateTime _dropOff;
        private int _numberOfAdults;
        private int _numberOfChildren;
        private bool _isMainDriver;
        private Guid? _voucherId;
        private string _drivingLicenceNumber;
        private string _driverForename;
        private string _driverSurname;
        private Voucher _voucher;
        private BookingStatus _status;
        private Guid _busId;
        private Bus _bus;
        private decimal _totalCost;

        public virtual string BookingNumber
        {
            get { return _bookingNumber; }
            set { _bookingNumber = value; }
        }

        public virtual Guid CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        public virtual Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        public virtual DateTime PickUp
        {
            get { return _pickUp; }
            set { _pickUp = value; }
        }

        public virtual DateTime DropOff
        {
            get { return _dropOff; }
            set { _dropOff = value; }
        }

        public virtual BookingStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public virtual Guid BusId
        {
            get { return _busId; }
            set { _busId = value; }
        }

        public virtual Bus Bus
        {
            get { return _bus; }
            set { _bus = value; }
        }

        public virtual decimal TotalCost
        {
            get { return _totalCost; }
            set { _totalCost = value; }
        }

        public virtual int NumberOfAdults
        {
            get { return _numberOfAdults; }
            set { _numberOfAdults = value; }
        }

        public virtual int NumberOfChildren
        {
            get { return _numberOfChildren; }
            set { _numberOfChildren = value; }
        }

        public virtual Guid? VoucherId
        {
            get { return _voucherId; }
            set { _voucherId = value; }
        }

        public virtual bool IsMainDriver
        {
            get { return _isMainDriver; }
            set { _isMainDriver = value; }
        }

        public virtual string DrivingLicenceNumber
        {
            get { return _drivingLicenceNumber; }
            set { _drivingLicenceNumber = value; }
        }

        public virtual string DriverForename
        {
            get { return _driverForename; }
            set { _driverForename = value; }
        }

        public virtual string DriverSurname
        {
            get { return _driverSurname; }
            set { _driverSurname = value; }
        }

        public virtual Voucher Voucher
        {
            get { return _voucher; }
            set { _voucher = value; }
        }

        public static ValidationMessageCollection ValidateAdminMake(AdminMakeBookingParameterSet parameterSet)
        {
            var validationMessages = ValidateMake(parameterSet);

            if (!parameterSet.IsMainDriver)
            {
                if (string.IsNullOrEmpty(parameterSet.DriverForename))
                {
                    validationMessages.AddError("DriverForename", "The forename of the main driver is required.");
                }

                if (string.IsNullOrEmpty(parameterSet.DriverSurname))
                {
                    validationMessages.AddError("DriverSurname", "The surname of the main driver is required.");
                }
            }

            if(!parameterSet.Status.HasValue)
            {
                validationMessages.AddError("Status", "The status is required.");
            }

            if(!parameterSet.TotalCost.HasValue)
            {
                validationMessages.AddError("TotalCost", "Total cost is required.");
            }
            else
            {
                if (parameterSet.TotalCost.Value == 0 && !parameterSet.WarningsAcknowledged)
                {
                    validationMessages.AddWarning("Total cost is zero.");
                }
            }

            return validationMessages;
        }

        public static ValidationMessageCollection ValidateCustomerMake(CustomerMakeBookingParameterSet parameterSet)
        {
            var validationMessages = ValidateMake(parameterSet);

            if(parameterSet.PickUp.HasValue 
                && parameterSet.PickUp.Value.DayOfWeek != DayOfWeek.Monday
                && parameterSet.PickUp.Value.DayOfWeek != DayOfWeek.Friday)
            {
                validationMessages.AddError("PickUp", "Pick up date must be a Monday or a Friday.");
            }

            if (parameterSet.DropOff.HasValue
                && parameterSet.DropOff.Value.DayOfWeek != DayOfWeek.Monday
                && parameterSet.DropOff.Value.DayOfWeek != DayOfWeek.Friday)
            {
                validationMessages.AddError("DropOff", "Drop off date must be a Monday or a Friday.");
            }
            
            if (!parameterSet.IsMainDriver)
            {
                if(string.IsNullOrEmpty(parameterSet.DriverForename))
                {
                    validationMessages.AddError("DriverForename", "If you are not the main driver, the forename of the main driver is required.");
                }

                if (string.IsNullOrEmpty(parameterSet.DriverSurname))
                {
                    validationMessages.AddError("DriverSurname", "If you are not the main driver, the surname of the main driver is required.");
                }
            }

            if(!string.IsNullOrEmpty(parameterSet.VoucherCode) && parameterSet.Voucher == null)
            {
                validationMessages.AddError("VoucherCode", "Voucher code is not valid.");
            }

            if(parameterSet.Voucher != null 
                && parameterSet.Voucher.ExpiryDate.HasValue 
                && parameterSet.Voucher.ExpiryDate.Value < DateTime.Now)
            {
                validationMessages.AddError("VoucherCode", "Voucher code has expired.");
            }

            if(!parameterSet.TermsAndConditionsAccepted)
            {
                validationMessages.AddError("TermsAndConditionsAccepted", "You must accept the Terms and Conditions.");
            }

            if (!parameterSet.RestrictionsAccepted)
            {
                validationMessages.AddError("RestrictionsAccepted", "Please check this box. If your trip does not meet these restrictions, please contact us directly to make a booking.");
            }

            return validationMessages;
        }

        protected static ValidationMessageCollection ValidateMake(MakeBookingParameterSet parameterSet)
        {
            var validationMessages = new ValidationMessageCollection();

            if (!parameterSet.PickUp.HasValue || parameterSet.PickUp.Value == default(DateTime))
            {
                validationMessages.AddError("PickUp", "Pick up date is required.");
            }
            else
            {
                if (parameterSet.PickUp.Value < DateTime.Now.Date) validationMessages.AddError("PickUp", "Pick up date must not be in the past.");
            }

            if (!parameterSet.DropOff.HasValue || parameterSet.DropOff == default(DateTime))
            {
                validationMessages.AddError("DropOff", "Drop off date is required.");
            }
            else
            {
                if (parameterSet.DropOff.Value < DateTime.Now) validationMessages.AddError("DropOff", "Drop off date must not be in the past.");
            }

            if (parameterSet.PickUp.HasValue && parameterSet.DropOff.HasValue)
            {
                if (parameterSet.DropOff.Value < parameterSet.PickUp.Value) validationMessages.AddError("DropOff", "Drop off date must not be before pickup date.");
            }

            if (parameterSet.Bus == null) validationMessages.AddError("Bus", "Bus is required.");
            if (parameterSet.NumberOfAdults <= 0) validationMessages.AddError("NumberOfAdults", "Booking must be for 1 or more adults.");

            if (string.IsNullOrEmpty(parameterSet.DrivingLicenceNumber))
            {
                validationMessages.AddError("DrivingLicenceNumber", "Driving licence number is required.");
            }

            if (parameterSet.Bus != null
                && parameterSet.PickUp.HasValue
                && parameterSet.DropOff.HasValue
                && parameterSet.Bus.GetConflictingBookings(parameterSet.PickUp.Value, parameterSet.DropOff.Value).Any())
            {
                validationMessages.AddError("", "Booking conflicts with existing bookings.");
            }

            var registerCustomerParameterSet = RegisterCustomerParameterSet.MapFrom(parameterSet);
            validationMessages.AddRange(Customer.ValidateRegister(registerCustomerParameterSet));

            return validationMessages;
        }

        public static Booking AdminMake(AdminMakeBookingParameterSet parameterSet)
        {
            var validationMessages = ValidateAdminMake(parameterSet);
            if (validationMessages.Any()) throw new ValidationException(validationMessages);
            var booking = Make(parameterSet);
            booking._status = parameterSet.Status.Value; //Won't be null because will have been caught by exception above.
            booking._totalCost = parameterSet.TotalCost.Value; //Won't be null because will have been caught by exception above.
            return booking;
        }

        public static Booking CustomerMake(CustomerMakeBookingParameterSet parameterSet)
        {
            var validationMessages = ValidateCustomerMake(parameterSet);
            if (validationMessages.Any()) throw new ValidationException(validationMessages);
            var booking = Make(parameterSet);
            booking._status = BookingStatus.Pending;
            booking._voucher = parameterSet.Voucher;
            var totalCostWithoutDiscount = parameterSet.Bus.GetUndiscountedRateFor(parameterSet.PickUp.Value, parameterSet.DropOff.Value);

            if (parameterSet.Voucher != null)
            {
                booking._totalCost = (1 - (parameterSet.Voucher.Discount/100))*totalCostWithoutDiscount;
            }
            else
            {
                booking._totalCost = totalCostWithoutDiscount;
            }

            return booking;
        }

        protected static Booking Make(MakeBookingParameterSet parameterSet)
        {
            var booking = new Booking();
            booking.Id = Guid.NewGuid();
            booking._pickUp = parameterSet.PickUp.Value; //Won't be null because will have been caught by exception above.
            booking._dropOff = parameterSet.DropOff.Value; //Won't be null because will have been caught by exception above.
            booking._bus = parameterSet.Bus;
            booking._numberOfAdults = parameterSet.NumberOfAdults;
            booking._numberOfChildren = parameterSet.NumberOfChildren;
            booking._isMainDriver = parameterSet.IsMainDriver;
            booking._drivingLicenceNumber = parameterSet.DrivingLicenceNumber;
            booking._driverForename = parameterSet.DriverForename;
            booking._driverSurname = parameterSet.DriverSurname;
            booking._createdBy = parameterSet.CurrentUser;
            var registerCustomerParameterSet = RegisterCustomerParameterSet.MapFrom(parameterSet);
            booking._customer = Customer.Register(registerCustomerParameterSet);
            booking._createdOn = parameterSet.CreatedOn;
            booking._deleted = false;
            parameterSet.Bus.Bookings.Add(booking);
            return booking;
        }

        public static Booking AdminMakeWithBookingNumber(AdminMakeBookingParameterSet parameterSet)
        {
            var booking = AdminMake(parameterSet);
            booking.BookingNumber = CalculateBookingNumber(parameterSet);
            return booking;
        }

        public static Booking CustomerMakeWithBookingNumber(CustomerMakeBookingParameterSet parameterSet)
        {
            var booking = CustomerMake(parameterSet);
            booking.BookingNumber = CalculateBookingNumber(parameterSet);
            return booking;
        }

        private static string CalculateBookingNumber(MakeBookingParameterSet parameterSet)
        {
            var bookingNumbers = parameterSet.OtherBookingsToday
                .Select(booking => Convert.ToInt32(booking.BookingNumber.Substring(8, 4)))
                .ToList();

            var nextBookingNumber = 1;

            if (bookingNumbers.Any())
            {
                nextBookingNumber = bookingNumbers.Max() + 1;
            }

            return String.Format("{0:yyyyMMdd}{1:0000}_{2}",
                                 parameterSet.CreatedOn,
                                 nextBookingNumber,
                                 parameterSet.Surname);
        }
    }
}
