using System;
using System.Collections.Generic;
using BusConductor.Domain.Common;

namespace BusConductor.Domain.Entities
{
    public class Role : Entity<Guid>
    {
        public virtual string Description { get; set; }
        public virtual string RoleName { get; set; }
        public virtual IList<PermissionRole> PermissionRoles { get; set; }
    }
}
