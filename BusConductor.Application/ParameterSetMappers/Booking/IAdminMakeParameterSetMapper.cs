using BusConductor.Application.Requests.Booking;
using BusConductor.Domain.ParameterSets;
using BusConductor.Domain.ParameterSets.Booking;

namespace BusConductor.Application.ParameterSetMappers.Booking
{
    public interface IAdminMakeParameterSetMapper
    {
        AdminMakeBookingParameterSet Map(AdminMakeBookingRequest request);
        AdminMakeBookingParameterSet MapWithOtherBookingsToday(AdminMakeBookingRequest request);
    }
}