using BusConductor.Application.Requests.Booking;
using BusConductor.Domain.ParameterSets;
using BusConductor.Domain.ParameterSets.Booking;

namespace BusConductor.Application.ParameterSetMappers.Booking
{
    public interface ICustomerMakeParameterSetMapper
    {
        CustomerMakeBookingParameterSet Map(CustomerMakeBookingRequest request);
        CustomerMakeBookingParameterSet MapWithOtherBookingsToday(CustomerMakeBookingRequest request);
    }
}