using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusConductor.UI.ViewModels.AdminBooking;

namespace BusConductor.UI.ViewModelMappers.AdminBooking
{
    public static class IndexViewModelMapper
    {
        public static IndexViewModel Map(IList<BusConductor.Domain.Entities.Booking> bookings)
        {
            var viewModel = new IndexViewModel();

            return viewModel;
        }
    }
}