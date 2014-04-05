using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusConductor.Application.Requests.Booking;
using BusConductor.UI.ViewModels.AdminBooking;
using Lucidity.Utilities;

namespace BusConductor.UI.ViewModelMappers.AdminBooking
{
    public static class MakeViewModelMapper
    {
        public static AdminMakeBookingRequest Map(MakeViewModel viewModel)
        {
            var request = PropertyMapper.MapMatchingProperties<MakeViewModel, AdminMakeBookingRequest>(viewModel);
            return request;
        }
    }
}