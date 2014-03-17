using System;
using BusConductor.Data.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.RepositoryContracts;

namespace BusConductor.Data.Repositories
{
    public class BusRepository : Repository<Bus, Guid>, IBusRepository
    {
        public BusRepository(IContextProvider contextProvider) :
            base(contextProvider)
        {
        }
    }
}
