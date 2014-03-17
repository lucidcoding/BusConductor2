using System;
using System.ComponentModel;

namespace BusConductor.UI.ViewModels.Booking
{
    public class ReviewViewModel
    {
        public Guid BusId { get; set; }

        [DisplayName("Selected camper")]
        public string BusName { get; set; }

        [DisplayName("Pick-up date")]
        public DateTime PickUp { get; set; }

        [DisplayName("Drop-off date")]
        public DateTime DropOff { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        [DisplayName("Address line 1")]
        public string AddressLine1 { get; set; }

        [DisplayName("Address line 2")]
        public string AddressLine2 { get; set; }

        [DisplayName("Address line 3")]
        public string AddressLine3 { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        [DisplayName("Post code")]
        public string PostCode { get; set; }

        [DisplayName("Email address")]
        public string Email { get; set; }

        [DisplayName("Telephone number")]
        public string TelephoneNumber { get; set; }

        public bool IsMainDriver { get; set; }

        [DisplayName("Driving licence number of driver")]
        public string DrivingLicenceNumber { get; set; }

        [DisplayName("Forename of driver")]
        public string DriverForename { get; set; }

        [DisplayName("Surname of driver")]
        public string DriverSurname { get; set; }

        [DisplayName("Number of adults")]
        public int NumberOfAdults { get; set; }

        [DisplayName("Number of children")]
        public int NumberOfChildren { get; set; }

        [DisplayName("Voucher code")]
        public string VoucherCode { get; set; }

        [DisplayName("Total Cost")]
        public decimal TotalCost { get; set; }

        public bool TermsAndConditionsAccepted { get; set; }

        public bool RestrictionsAccepted { get; set; }
    }
}