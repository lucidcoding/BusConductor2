using System;
using BusConductor.Application.Contracts;
using BusConductor.Application.Requests;
using BusConductor.Data.Common;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.RepositoryContracts;

namespace BusConductor.Application.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskTypeRepository _taskTypeRepository;
        private readonly IUserRepository _userRepository;

        public TaskService(
            ITaskRepository taskRepository,
            ITaskTypeRepository taskTypeRepository,
            IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _taskTypeRepository = taskTypeRepository;
            _userRepository = userRepository;
        }

        public void Complete(Guid id, string userName)
        {
            var task = _taskRepository.GetById(id);
            var currentUser = _userRepository.GetByUsername(userName);
            task.Complete("test", currentUser);
        }

        public void Cancel(Guid id, string userName)
        {
            var task = _taskRepository.GetById(id);
            var currentUser = _userRepository.GetByUsername(userName);
            task.Cancel(currentUser);
        }

        public void Reassign(Guid taskId, Guid? reassignedToId, string userName)
        {
            var task = _taskRepository.GetById(taskId);
            var userToReassignTo = reassignedToId.HasValue ? _userRepository.GetById(reassignedToId.Value) : null;
            var currentUser = _userRepository.GetByUsername(userName);
            task.Reassign(userToReassignTo, currentUser);
        }

        public ValidationMessageCollection ValidateRaise(RaiseTaskRequest request)
        {
            var assignedTo = request.AssignedToId.HasValue ? _userRepository.GetById(request.AssignedToId.Value) : null;
            var type = request.TypeId.HasValue ? _taskTypeRepository.GetById(request.TypeId.Value) : null;
            var currentUser = _userRepository.GetByUsername(request.UserName);
            var validationMessages = Task.ValidateRaise(request.Description, assignedTo, type, currentUser);
            return validationMessages;
        }

        public Guid Raise(RaiseTaskRequest request)
        {
            var assignedTo = request.AssignedToId.HasValue ? _userRepository.GetById(request.AssignedToId.Value) : null;
            var type = request.TypeId.HasValue ? _taskTypeRepository.GetById(request.TypeId.Value) : null;
            var currentUser = _userRepository.GetByUsername(request.UserName);
            var task = Task.Raise(request.Description, assignedTo, type, currentUser);
            _taskRepository.Save(task);
            return task.Id.Value;
        }

        public ValidationMessageCollection ValidateEdit(EditTaskRequest request)
        {
            var task = _taskRepository.GetById(request.Id);
            var assignedTo = request.AssignedToId.HasValue ? _userRepository.GetById(request.AssignedToId.Value) : null;
            var type = request.TypeId.HasValue ? _taskTypeRepository.GetById(request.TypeId.Value) : null;
            var currentUser = _userRepository.GetByUsername(request.UserName);
            var validationMessages = task.ValidateEdit(request.Description, assignedTo, type, currentUser);
            return validationMessages;
        }

        public void Edit(EditTaskRequest request)
        {
            var task = _taskRepository.GetById(request.Id);
            var assignedTo = request.AssignedToId.HasValue ? _userRepository.GetById(request.AssignedToId.Value) : null;
            var type = request.TypeId.HasValue ? _taskTypeRepository.GetById(request.TypeId.Value) : null;
            var currentUser = _userRepository.GetByUsername(request.UserName);
            task.Edit(request.Description, assignedTo, type, currentUser);
            _taskRepository.Update(task);
        }
    }
}
