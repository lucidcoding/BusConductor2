using System;
using BusConductor.Application.Requests.Booking;
using BusConductor.Domain.ParameterSets;
using BusConductor.Domain.ParameterSets.Booking;
using BusConductor.Domain.RepositoryContracts;
using Lucidity.Utilities;

namespace BusConductor.Application.ParameterSetMappers.Booking
{
    public class AdminMakeParameterSetMapper : IAdminMakeParameterSetMapper
    {
        private readonly IBusRepository _busRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;

        public AdminMakeParameterSetMapper(
            IBusRepository busRepository,
            IUserRepository userRepository,
            IBookingRepository bookingRepository)
        {
            _busRepository = busRepository;
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
        }

        public AdminMakeBookingParameterSet Map(AdminMakeBookingRequest request)
        {
            var parameterSet = PropertyMapper.MapMatchingProperties<AdminMakeBookingRequest, AdminMakeBookingParameterSet>(request);
            parameterSet.CreatedOn = DateTime.Now;
            parameterSet.Bus = _busRepository.GetById(request.BusId);
            parameterSet.CurrentUser = _userRepository.GetById(request.CurrentUserId);
            return parameterSet;
        }

        public AdminMakeBookingParameterSet MapWithOtherBookingsToday(AdminMakeBookingRequest request)
        {
            var parameterSet = Map(request);
            parameterSet.OtherBookingsToday = _bookingRepository.GetByDate(parameterSet.CreatedOn);
            return parameterSet;
        }
    }
}
