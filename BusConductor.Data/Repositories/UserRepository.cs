using System;
using System.Linq;
using BusConductor.Data.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.RepositoryContracts;

namespace BusConductor.Data.Repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(IContextProvider contextProvider) :
            base(contextProvider)
        {
        }

        public User GetByUsername(string username)
        {
            return Context
                .Users
                .SingleOrDefault(user => user.Username == username);
        }
    }
}
