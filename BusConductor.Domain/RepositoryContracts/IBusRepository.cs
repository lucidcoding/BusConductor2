using System;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;

namespace BusConductor.Domain.RepositoryContracts
{
    public interface IBusRepository : IRepository<Bus, Guid>
    {
    }
}
