namespace BusConductor.Application.Requests.Booking
{
    public class CustomerMakeBookingRequest : MakeBookingRequest
    {
        public string VoucherCode { get; set; }
        public bool TermsAndConditionsAccepted { get; set; }
        public bool RestrictionsAccepted { get; set; }
    }
}
