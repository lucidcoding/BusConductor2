using System;
using BusConductor.Domain.Common;

namespace BusConductor.Domain.Entities
{
    public class User : Entity<Guid>
    {
        private string _username;
        private Guid _roleId;
        private Role _role;

        public virtual string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public virtual Guid RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }

        public virtual Role Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public static User Create(string username, Role role, User currentUser)
        {
            var user = new User();
            user._id = Guid.NewGuid();
            user._username = username;
            user._role = role;
            user._createdOn = DateTime.Now;
            user._createdBy = currentUser;
            user._deleted = false;
            return user;
        }
    }
}
