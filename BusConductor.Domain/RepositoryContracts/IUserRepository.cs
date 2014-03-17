using System;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;

namespace BusConductor.Domain.RepositoryContracts
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        User GetByUsername(string userName);
    }
}

