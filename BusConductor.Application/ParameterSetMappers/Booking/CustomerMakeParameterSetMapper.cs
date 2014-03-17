using System;
using BusConductor.Application.Requests.Booking;
using BusConductor.Domain.ParameterSets;
using BusConductor.Domain.ParameterSets.Booking;
using BusConductor.Domain.RepositoryContracts;
using Lucidity.Utilities;

namespace BusConductor.Application.ParameterSetMappers.Booking
{
    public class CustomerMakeParameterSetMapper : ICustomerMakeParameterSetMapper
    {
        private readonly IBusRepository _busRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IBookingRepository _bookingRepository;

        public CustomerMakeParameterSetMapper(
            IBusRepository busRepository,
            IUserRepository userRepository,
            IVoucherRepository voucherRepository,
            IBookingRepository bookingRepository)
        {
            _busRepository = busRepository;
            _userRepository = userRepository;
            _voucherRepository = voucherRepository;
            _bookingRepository = bookingRepository;
        }

        public CustomerMakeBookingParameterSet Map(CustomerMakeBookingRequest request)
        {
            var parameterSet = PropertyMapper.MapMatchingProperties<CustomerMakeBookingRequest, CustomerMakeBookingParameterSet>(request);
            parameterSet.CreatedOn = DateTime.Now;
            parameterSet.Bus = _busRepository.GetById(request.BusId);
            parameterSet.Voucher = !string.IsNullOrEmpty(request.VoucherCode) ? _voucherRepository.GetByCode(request.VoucherCode) : null;
            parameterSet.CurrentUser = _userRepository.GetByUsername("Application");
            return parameterSet;
        }

        public CustomerMakeBookingParameterSet MapWithOtherBookingsToday(CustomerMakeBookingRequest request)
        {
            var parameterSet = Map(request);
            parameterSet.OtherBookingsToday = _bookingRepository.GetByDate(parameterSet.CreatedOn);
            return parameterSet;
        }
    }
}
