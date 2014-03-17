using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using BusConductor.Data.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.RepositoryContracts;

namespace BusConductor.Data.Repositories
{
    public class BookingRepository : Repository<Booking, Guid>, IBookingRepository
    {
        public BookingRepository(IContextProvider contextProvider) :
            base(contextProvider)
        {
        }

        public IList<Booking> GetByDate(DateTime date)
        {
            return Context
                .Bookings
                .Where(booking => EntityFunctions.TruncateTime(booking.CreatedOn) == date.Date)
                .ToList();
        }
    }
}
