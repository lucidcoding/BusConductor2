using System;
using System.Linq;
using BusConductor.Data.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.RepositoryContracts;

namespace BusConductor.Data.Repositories
{
    public class RoleRepository : Repository<Role, Guid>, IRoleRepository
    {
        public RoleRepository(IContextProvider contextProvider) :
            base(contextProvider)
        {
        }

        public Role GetByName(string roleName)
        {
            return Context
                .Roles
                .SingleOrDefault(user => user.RoleName == roleName);
        }
    }
}
