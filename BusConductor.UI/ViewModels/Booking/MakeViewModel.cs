using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusConductor.UI.ViewModels.Calendar;

namespace BusConductor.UI.ViewModels.Booking
{
    public class MakeViewModel //: IValidatableObject
    {
        public Guid BusId { get; set; }

        public DisplayMonthViewModel Calendar { get; set; }

        [DisplayName("Selected camper")]
        public string BusName { get; set; }

        [DisplayName("Pick-up date")]
        public DateTime? PickUp { get; set; }

        [DisplayName("Drop-off date")]
        public DateTime? DropOff { get; set; }

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

        [Compare("Email")]
        [DisplayName("Confirm email address")]
        public string ConfirmEmail { get; set; }

        [DisplayName("Telephone number")]
        public string TelephoneNumber { get; set; }

        [DisplayName("Please check this box if you are the main driver")]
        public bool IsMainDriver { get; set; }

        public string AlternateDriverAdditionalClasses { get; set; }

        [DisplayName("Driving licence number of driver")]
        public string DrivingLicenceNumber { get; set; }

        [DisplayName("Forename of driver")]
        public string DriverForename { get; set; }

        [DisplayName("Surname of driver")]
        public string DriverSurname { get; set; }

        [DisplayName("Number of adults")]
        public int NumberOfAdults { get; set; }

        public SelectList NumberOfAdultsOptions { get; set; }

        [DisplayName("Number of children")]
        public int NumberOfChildren { get; set; }

        public SelectList NumberOfChildrenOptions { get; set; }

        [DisplayName("Voucher code")]
        public string VoucherCode { get; set; }

        [DisplayName("Please confirm you accept the Terms and Conditions")]
        public bool TermsAndConditionsAccepted { get; set; }

        [DisplayName("Please confirm there will be only one driver, aged 25 to 75, and you do not intend to leave the UK")]
        public bool RestrictionsAccepted { get; set; }
    }
}