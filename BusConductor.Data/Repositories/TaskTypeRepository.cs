using System;
using BusConductor.Data.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.RepositoryContracts;

namespace BusConductor.Data.Repositories
{
    public class TaskTypeRepository : Repository<TaskType, Guid>, ITaskTypeRepository
    {
        public TaskTypeRepository(IContextProvider contextProvider) :
            base(contextProvider)
        {
        }
    }
}
