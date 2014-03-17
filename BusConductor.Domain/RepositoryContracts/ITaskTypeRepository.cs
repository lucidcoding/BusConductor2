using System;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;

namespace BusConductor.Domain.RepositoryContracts
{
    public interface ITaskTypeRepository : IRepository<TaskType, Guid>
    {
    }
}
