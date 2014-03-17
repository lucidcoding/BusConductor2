using System;
using BusConductor.Application.Requests.Booking;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;

namespace BusConductor.Application.Contracts
{
    public interface IBookingService
    {
        ValidationMessageCollection ValidateCustomerMake(CustomerMakeBookingRequest request);
        Booking SummarizeCustomerMake(CustomerMakeBookingRequest request);
        string CustomerMake(CustomerMakeBookingRequest request);
        ValidationMessageCollection ValidateAdminMake(AdminMakeBookingRequest request);
        Booking SummarizeAdminMake(AdminMakeBookingRequest request);
        string AdminMake(AdminMakeBookingRequest request);
    }
}
