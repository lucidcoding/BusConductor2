using System;
using BusConductor.Application.Requests;
using BusConductor.Domain.Common;

namespace BusConductor.Application.Contracts
{
    public interface ITaskService
    {
        void Complete(Guid id, string userName);
        void Cancel(Guid id, string userName);
        void Reassign(Guid taskId, Guid? reassignedToId, string userName);
        ValidationMessageCollection ValidateRaise(RaiseTaskRequest request);
        Guid Raise(RaiseTaskRequest request);
        ValidationMessageCollection ValidateEdit(EditTaskRequest request);
        void Edit(EditTaskRequest request);
    }
}
