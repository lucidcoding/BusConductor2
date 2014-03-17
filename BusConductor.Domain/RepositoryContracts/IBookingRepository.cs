using System;
using System.Collections.Generic;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;

namespace BusConductor.Domain.RepositoryContracts
{
    public interface IBookingRepository : IRepository<Booking, Guid>
    {
        IList<Booking> GetByDate(DateTime date);
    }
}
