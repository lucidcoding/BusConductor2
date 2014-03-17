using System;
using BusConductor.Domain.Enumerations;

namespace BusConductor.Application.Requests.Booking
{
    public class AdminMakeBookingRequest : MakeBookingRequest
    {
        public BookingStatus? Status { get; set; }
        public decimal? TotalCost { get; set; }
        public bool WarningsAcknowledged { get; set; }
        public Guid CurrentUserId { get; set; }
    }
}
