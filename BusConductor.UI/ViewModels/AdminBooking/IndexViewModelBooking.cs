using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusConductor.Domain.Enumerations;

namespace BusConductor.UI.ViewModels.AdminBooking
{
    public class IndexViewModelBooking
    {
        public string BookingNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime PickUp { get; set; }
        public DateTime DropOff { get; set; }
        public BookingStatus Status { get; set; }
        public string BusName { get; set; }
        public decimal TotalCost { get; set; }
    }
}