using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusConductor.Domain.Common;
using BusConductor.Domain.Enumerations;
using BusConductor.Domain.ParameterSets.Enquiry;
using BusConductor.Domain.ParameterSets.Customer;
using System.Text.RegularExpressions;

namespace BusConductor.Domain.Entities
{
    public class Enquiry : Entity<Guid>
    {
        private string _forename;
        private string _surname;
        private string _addressLine1;
        private string _addressLine2;
        private string _addressLine3;
        private string _town;
        private string _county;
        private string _postCode;
        private string _email;
        private string _telephoneNumber;
        private DateTime _pickUp;
        private DateTime _dropOff;
        private int _numberOfAdults;
        private int _numberOfChildren;
        private bool _isMainDriver;
        private string _drivingLicenceNumber;
        private string _driverForename;
        private string _driverSurname;
        private Guid _busId;
        private Bus _bus;
        private Guid? _resultingBookingId;
        private Booking _resultingBooking;
        private EnquiryStatus _status;

        public virtual string Forename
        {
            get { return _forename; }
            set { _forename = value; }
        }

        public virtual string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public virtual string TelephoneNumber
        {
            get { return _telephoneNumber; }
            set { _telephoneNumber = value; }
        }

        public virtual string AddressLine1
        {
            get { return _addressLine1; }
            set { _addressLine1 = value; }
        }

        public virtual string AddressLine2
        {
            get { return _addressLine2; }
            set { _addressLine2 = value; }
        }

        public virtual string AddressLine3
        {
            get { return _addressLine3; }
            set { _addressLine3 = value; }
        }

        public virtual string Town
        {
            get { return _town; }
            set { _town = value; }
        }

        public virtual string County
        {
            get { return _county; }
            set { _county = value; }
        }

        public virtual string PostCode
        {
            get { return _postCode; }
            set { _postCode = value; }
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

        public virtual Guid? ResultingBookingId
        {
            get { return _resultingBookingId; }
            set { _resultingBookingId = value; }
        }

        public virtual Booking ResultingBooking
        {
            get { return _resultingBooking; }
            set { _resultingBooking = value; }
        }

        public EnquiryStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public static ValidationMessageCollection ValidateMake(MakeParameterSet parameterSet)
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

            if (string.IsNullOrEmpty(parameterSet.Forename))
            {
                validationMessages.AddError("Forename", "Forename is required.");
            }
            else
            {
                if (parameterSet.Forename.Length > 50) validationMessages.AddError("Forename", "Forename must be 50 characters or less.");
            }

            if (string.IsNullOrEmpty(parameterSet.Forename))
            {
                validationMessages.AddError("Surname", "Surname is required.");
            }
            else
            {
                if (parameterSet.Surname.Length > 50) validationMessages.AddError("Surname", "Surname must be 50 characters or less.");
            }

            if (string.IsNullOrEmpty(parameterSet.AddressLine1))
            {
                validationMessages.AddError("AddressLine1", "Address line 1 is required.");
            }
            else
            {
                if (parameterSet.AddressLine1.Length > 50) validationMessages.AddError("AddressLine1", "Address line 1 must be 50 characters or less.");
            }

            if (parameterSet.AddressLine2 != null && parameterSet.AddressLine2.Length > 50) validationMessages.AddError("AddressLine2", "Address line 2 must be 50 characters or less.");
            if (parameterSet.AddressLine3 != null && parameterSet.AddressLine3.Length > 50) validationMessages.AddError("AddressLine3", "Address line 3 must be 50 characters or less.");

            if (string.IsNullOrEmpty(parameterSet.Town))
            {
                validationMessages.AddError("Town", "Town is required.");
            }
            else
            {
                if (parameterSet.Town.Length > 50) validationMessages.AddError("Town", "Town must be 50 characters or less.");
            }

            if (parameterSet.County != null && parameterSet.County.Length > 50) validationMessages.AddError("County", "County must be 50 characters or less.");

            var rxPostCode =
                new Regex(
                    @"^(([gG][iI][rR] {0,}0[aA]{2})|((([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y]?[0-9][0-9]?)|(([a-pr-uwyzA-PR-UWYZ][0-9][a-hjkstuwA-HJKSTUW])|([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y][0-9][abehmnprv-yABEHMNPRV-Y]))) {0,}[0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2}))$");

            if (string.IsNullOrEmpty(parameterSet.PostCode))
            {
                validationMessages.AddError("PostCode", "Post code is required.");
            }
            else
            {
                if (!rxPostCode.IsMatch(parameterSet.PostCode)) validationMessages.AddError("PostCode", "Post code is not valid.");
            }

            if (string.IsNullOrEmpty(parameterSet.Email))
            {
                validationMessages.AddError("Email", "Email is required.");
            }
            else if (parameterSet.Email.Length > 50)
            {
                validationMessages.AddError("Email", "Email must be 50 characters or less.");
            }
            else
            {
                var rxNonStrictEmail = new Regex(@"[A-Za-z0-9\.-_\+]+@[A-Za-z0-9\.-_\+]+");
                if (!rxNonStrictEmail.IsMatch(parameterSet.Email)) validationMessages.AddError("Email", "Email is not valid.");
            }

            if (string.IsNullOrEmpty(parameterSet.TelephoneNumber))
            {
                validationMessages.AddError("TelephoneNumber", "Telephone number is required.");
            }
            else
            {
                if (parameterSet.TelephoneNumber.Length > 50) validationMessages.AddError("TelephoneNumber", "Telephone number must be 50 characters or less.");
            }

            return validationMessages;
        }

        public static Enquiry Make(MakeParameterSet parameterSet)
        {
            var enquiry = new Enquiry();
            enquiry._id = Guid.NewGuid();
            enquiry._forename = parameterSet.Forename;
            enquiry._surname = parameterSet.Surname;
            enquiry._addressLine1 = parameterSet.AddressLine1;
            enquiry._addressLine2 = parameterSet.AddressLine2;
            enquiry._addressLine3 = parameterSet.AddressLine3;
            enquiry._town = parameterSet.Town;
            enquiry._county = parameterSet.County;
            enquiry._postCode = parameterSet.PostCode;
            enquiry._email = parameterSet.Email;
            enquiry._telephoneNumber = parameterSet.TelephoneNumber;
            enquiry._pickUp = parameterSet.PickUp.Value;
            enquiry._dropOff = parameterSet.DropOff.Value;
            enquiry._numberOfAdults = parameterSet.NumberOfAdults;
            enquiry._numberOfChildren = parameterSet.NumberOfChildren;
            enquiry._isMainDriver = parameterSet.IsMainDriver;
            enquiry._drivingLicenceNumber = parameterSet.DrivingLicenceNumber;
            enquiry._driverForename = parameterSet.DriverForename;
            enquiry._driverSurname = parameterSet.DriverSurname;
            enquiry._bus = parameterSet.Bus;
            enquiry._status = EnquiryStatus.NotFollowed;
            enquiry._createdOn = DateTime.Now;
            enquiry._createdBy = parameterSet.CurrentUser;
            return enquiry;
        }
    }
}
