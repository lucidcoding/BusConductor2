using System;
using System.Linq;
using BusConductor.Domain.Common;
using BusConductor.Domain.Constants;
using BusConductor.Domain.Enumerations;

namespace BusConductor.Domain.Entities
{
    public class Task : Entity<Guid>
    {
        public virtual string Description { get; set; }
        public virtual Guid? AssignedToId { get; set; }
        public virtual User AssignedTo { get; set; }
        public virtual TaskType Type { get; set; }
        public virtual DateTime DueDate { get; set; }
        public virtual TaskStatus Status { get; set; }
        public virtual DateTime? CompletedOn { get; set; }
        public virtual string CompletedMessage { get; set; }

        public virtual RagStatus RagStatus
        {
            get
            {   
                var now = DateTime.Now;

                if(now > DueDate)
                {
                    return RagStatus.Red;
                }

                if(now > DueDate.AddMinutes(0 - Type.WarningWindowMinutes))
                {
                    return RagStatus.Amber;
                }

                return RagStatus.Green;
            }
        }

        public static ValidationMessageCollection ValidateRaise(string description, User assignedTo, TaskType type, User currentUser)
        {
            //ValidateRaise and ValidateEdit look similar. They could be refactored to call the same method but I have left it like this
            //in this example for clarity.
            var validationMessages = new ValidationMessageCollection();

            if(string.IsNullOrEmpty(description))
            {
                validationMessages.AddError("Description", "Description is null or empty.");
            }

            if(assignedTo == null)
            {
                validationMessages.AddError("AssignedTo", "AssignedTo is null.");
            }

            if (type == null)
            {
                validationMessages.AddError("Type", "Type is null.");
            }

            return validationMessages;
        }

        public static Task Raise(string description, User assignedTo, TaskType type, User currentUser)
        {
            var validationMessages = ValidateRaise(description, assignedTo, type, currentUser);

            if (validationMessages.Any())
            {
                throw new ValidationException(validationMessages);
            }

            var now = DateTime.Now;

            var task = new Task
                       {
                           Id = Guid.NewGuid(),
                           Description = description,
                           AssignedTo = assignedTo,
                           Type = type,
                           DueDate = now.AddMinutes(type.ServiceLevelAgreementMinutes),
                           Status = TaskStatus.InProgress,
                           CreatedOn = now,
                           CreatedBy = currentUser,
                           Deleted = false
                       };

            return task;
        }

        public virtual ValidationMessageCollection ValidateEdit(string description, User assignedTo, TaskType type, User currentUser)
        {
            //ValidateRaise and ValidateEdit look similar. They could be refactored to call the same method but I have left it like this
            //in this example for clarity.
            var validationMessages = new ValidationMessageCollection();

            if (string.IsNullOrEmpty(description))
            {
                validationMessages.AddError("Description", "Description is null or empty.");
            }

            if (assignedTo == null)
            {
                validationMessages.AddError("AssignedTo", "AssignedTo is null.");
            }

            if (type == null)
            {
                validationMessages.AddError("Type", "AssignedTo is null.");
            }

            return validationMessages;
        }

        public virtual void Edit(string description, User assignedTo, TaskType type, User currentUser)
        {
            Description = description;
            AssignedTo = assignedTo;
            Type = type;
            DueDate = CreatedOn.AddMinutes(type.ServiceLevelAgreementMinutes);
            LastModifiedBy = currentUser;
            LastModifiedOn = DateTime.Now;
        }

        public virtual ValidationMessageCollection ValidateComplete(string completedMessage, User currentUser)
        {
            var validationMessages = new ValidationMessageCollection();

            if(string.IsNullOrEmpty(completedMessage))
            {
                validationMessages.AddError("CompletedMessage", "Message is empty.");
            }

            //Business logic is that you can't complete someone else's task unless you have the permission to do so.
            if(AssignedTo != currentUser 
                && !currentUser.Role.PermissionRoles.Select(x => x.Permission.Id).Contains(PermissionIds.CompleteAnotherUsersTask))
            {
                validationMessages.AddError("You do not have the permission to complete this task.");
            }

            return validationMessages;
        }

        public virtual void Complete(string completedMessage, User currentUser)
        {
            var now = DateTime.Now;
            Status = TaskStatus.Completed;
            CompletedOn = now;
            LastModifiedBy = currentUser;
            LastModifiedOn = now;
        }

        public virtual void Cancel(User currentUser)
        {
            Status = TaskStatus.Cancelled;
            LastModifiedBy = currentUser;
            LastModifiedOn = DateTime.Now;
        }

        public virtual void Reassign(User reassignedTo, User currentUser)
        {
            AssignedTo = reassignedTo;
            LastModifiedBy = currentUser;
            LastModifiedOn = DateTime.Now;
        }
    }
}
