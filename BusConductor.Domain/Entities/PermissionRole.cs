using System;
using BusConductor.Domain.Common;

namespace BusConductor.Domain.Entities
{
    public class PermissionRole : Entity<Guid>
    {
        public virtual Guid PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
