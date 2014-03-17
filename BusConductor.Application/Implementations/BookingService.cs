using System;
using BusConductor.Application.Contracts;
using BusConductor.Application.ParameterSetMappers.Booking;
using BusConductor.Application.Requests.Booking;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.RepositoryContracts;
using Lucidity.Utilities.Contracts.Logging;

namespace BusConductor.Application.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerMakeParameterSetMapper _customerMakePendingParameterSetMapper;
        private readonly IAdminMakeParameterSetMapper _adminMakeParameterSetMapper;
        private readonly ILog _log;

        public BookingService(
            IBookingRepository bookingRepository,
            ICustomerMakeParameterSetMapper customerMakePendingParameterSetMapper,
            IAdminMakeParameterSetMapper adminMakeParameterSetMapper,
            ILog log)
        {
            _bookingRepository = bookingRepository;
            _customerMakePendingParameterSetMapper = customerMakePendingParameterSetMapper;
            _adminMakeParameterSetMapper = adminMakeParameterSetMapper;
            _log = log;
        }

        public ValidationMessageCollection ValidateCustomerMake(CustomerMakeBookingRequest request)
        {
            _log.Add(request);
            var parameterSet = _customerMakePendingParameterSetMapper.Map(request);
            var validationMessages = Booking.ValidateCustomerMake(parameterSet);
            return validationMessages;
        }

        public Booking SummarizeCustomerMake(CustomerMakeBookingRequest request)
        {
            _log.Add(request);
            var parameterSet = _customerMakePendingParameterSetMapper.Map(request);
            var booking = Booking.CustomerMake(parameterSet);
            return booking;
        }

        public string CustomerMake(CustomerMakeBookingRequest request)
        {
            _log.Add(request);
            var parameterSet = _customerMakePendingParameterSetMapper.MapWithOtherBookingsToday(request);
            var booking = Booking.CustomerMakeWithBookingNumber(parameterSet);
            _bookingRepository.Save(booking);
            return booking.BookingNumber;
        }

        public ValidationMessageCollection ValidateAdminMake(AdminMakeBookingRequest request)
        {
            _log.Add(request);
            var parameterSet = _adminMakeParameterSetMapper.Map(request);
            var validationMessages = Booking.ValidateAdminMake(parameterSet);
            return validationMessages;
        }

        public Booking SummarizeAdminMake(AdminMakeBookingRequest request)
        {
            _log.Add(request);
            var parameterSet = _adminMakeParameterSetMapper.Map(request);
            var booking = Booking.AdminMake(parameterSet);
            return booking;
        }

        public string AdminMake(AdminMakeBookingRequest request)
        {
            _log.Add(request);
            var parameterSet = _adminMakeParameterSetMapper.MapWithOtherBookingsToday(request);
            var booking = Booking.AdminMakeWithBookingNumber(parameterSet);
            _bookingRepository.Save(booking);
            return booking.BookingNumber;
        }
    }
}
