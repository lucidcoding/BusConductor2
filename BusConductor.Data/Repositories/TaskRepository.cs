using System;
using System.Collections.Generic;
using BusConductor.Data.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.Enumerations;
using BusConductor.Domain.RepositoryContracts;

namespace BusConductor.Data.Repositories
{
    public class TaskRepository : Repository<Task, Guid>, ITaskRepository
    {
        public TaskRepository(IContextProvider contextProvider) :
            base(contextProvider)
        {
        }
    }
}
