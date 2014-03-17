using System;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;

namespace BusConductor.Domain.RepositoryContracts
{
    public interface IRoleRepository : IRepository<Role, Guid>
    {
        Role GetByName(string roleName);
    }
}
