using System;
using BusConductor.Domain.Common;

namespace BusConductor.Domain.Entities
{
    public class Permission : Entity<Guid>
    {
        public virtual string Description { get; set; }
    }
}
