using BusConductor.Application.Requests.Booking;
using BusConductor.UI.ViewModels.Booking;

namespace BusConductor.UI.ViewModelMappers.Booking
{
    public static class ReviewViewModelMapper
    {
        public static ReviewViewModel Map(Domain.Entities.Booking booking)
        {
            var viewModel = new ReviewViewModel();
            viewModel.BusId = booking.Bus.Id.Value;
            viewModel.BusName = booking.Bus.Name;
            viewModel.PickUp = booking.PickUp;
            viewModel.DropOff = booking.DropOff;
            viewModel.Forename = booking.Customer.Forename;
            viewModel.Surname = booking.Customer.Surname;
            viewModel.AddressLine1 = booking.Customer.AddressLine1;
            viewModel.AddressLine2 = booking.Customer.AddressLine2;
            viewModel.AddressLine3 = booking.Customer.AddressLine3;
            viewModel.Town = booking.Customer.Town;
            viewModel.County = booking.Customer.County;
            viewModel.PostCode = booking.Customer.PostCode;
            viewModel.Email = booking.Customer.Email;
            viewModel.TelephoneNumber = booking.Customer.TelephoneNumber;
            viewModel.IsMainDriver = booking.IsMainDriver;
            viewModel.DrivingLicenceNumber = booking.DrivingLicenceNumber;
            viewModel.DriverForename = booking.DriverForename;
            viewModel.DriverSurname = booking.DriverSurname;
            viewModel.NumberOfAdults = booking.NumberOfAdults;
            viewModel.NumberOfChildren = booking.NumberOfChildren;
            viewModel.TotalCost = booking.TotalCost;
            viewModel.VoucherCode = booking.Voucher != null ? booking.Voucher.Code : null;
            return viewModel;
        }

        public static CustomerMakeBookingRequest Map(ReviewViewModel viewModel)
        {
            var request = new CustomerMakeBookingRequest();
            request.BusId = viewModel.BusId;
            request.PickUp = viewModel.PickUp;
            request.DropOff = viewModel.DropOff;
            request.Forename = viewModel.Forename;
            request.Surname = viewModel.Surname;
            request.AddressLine1 = viewModel.AddressLine1;
            request.AddressLine2 = viewModel.AddressLine2;
            request.AddressLine3 = viewModel.AddressLine3;
            request.Town = viewModel.Town;
            request.County = viewModel.County;
            request.PostCode = viewModel.PostCode;
            request.Email = viewModel.Email;
            request.TelephoneNumber = viewModel.TelephoneNumber;
            request.IsMainDriver = viewModel.IsMainDriver;
            request.DrivingLicenceNumber = viewModel.DrivingLicenceNumber;
            request.DriverForename = viewModel.DriverForename;
            request.DriverSurname = viewModel.DriverSurname;
            request.NumberOfAdults = viewModel.NumberOfAdults;
            request.NumberOfChildren = viewModel.NumberOfChildren;
            request.VoucherCode = viewModel.VoucherCode;
            request.TermsAndConditionsAccepted = viewModel.TermsAndConditionsAccepted;
            request.RestrictionsAccepted = viewModel.RestrictionsAccepted;
            return request;
        }
    }
}