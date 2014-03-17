using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusConductor.Application.Requests.Enquiry
{
    public class MakeRequest
    {
        public DateTime? PickUp { get; set; }
        public DateTime? DropOff { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public bool IsMainDriver { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string DrivingLicenceNumber { get; set; }
        public string DriverForename { get; set; }
        public string DriverSurname { get; set; }
        public Guid BusId { get; set; }
    }
}
