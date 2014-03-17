using System;
using System.Collections.Generic;
using NUnit.Framework;
using BusConductor.Domain.Constants;
using BusConductor.Domain.Entities;

namespace BusConductor.Domain.UnitTests.Entities.TaskTests
{
    [TestFixture]
    public class ValidateCompleteTests
    {
        [Test]
        public void Given_I_supply_valid_parameters_When_assigned_user_calls_ValidateComplete_task_Then_no_messages_are_returned()
        {
            var assignedTo = new User { Id = Guid.NewGuid() };
            var taskType = new TaskType { WarningWindowMinutes = 10 };
            var task = Task.Raise("Test task for completion 1", assignedTo, taskType, assignedTo);
            var validationMessages = task.ValidateComplete("Completion message 1", assignedTo);
            Assert.That(validationMessages.Count, Is.EqualTo(0));
        }

        [Test]
        public void Given_user_is_not_assignee_and_does_not_have_permissions_to_complete_another_users_tasks_When_user_calls_ValidateComplete_task_Then_messages_are_returned()
        {
            var assignedTo = new User{ Id = Guid.NewGuid() };
            var taskType = new TaskType { WarningWindowMinutes = 10 };

            var completingUser = new User
                                     {
                                         Id = Guid.NewGuid(),
                                         Role = new Role
                                                    {
                                                        Id = Guid.NewGuid(),
                                                        PermissionRoles = new List<PermissionRole>()
                                                    }
                                     };

            var task = Task.Raise("Test task for completion 1", assignedTo, taskType, assignedTo);
            var validationMessages = task.ValidateComplete("Completion message 1", completingUser);
            Assert.That(validationMessages.Count, Is.EqualTo(1));
            Assert.That(validationMessages[0].Text, Is.EqualTo("You do not have the permission to complete this task."));
        }

        [Test]
        public void Given_user_is_not_assignee_and_does_have_permissions_to_complete_another_users_tasks_When_user_calls_ValidateComplete_task_Then_no_messages_are_returned()
        {
            var assignedTo = new User { Id = Guid.NewGuid() };
            var taskType = new TaskType { WarningWindowMinutes = 10 };

            var completingUser = new User
                                     {
                                         Id = Guid.NewGuid(),
                                         Role = new Role
                                                    {
                                                        Id = Guid.NewGuid(),
                                                        PermissionRoles = new List<PermissionRole>
                                                                              {
                                                                                  new PermissionRole
                                                                                      {
                                                                                          Permission = new Permission
                                                                                                           {
                                                                                                               Id = PermissionIds.CompleteAnotherUsersTask
                                                                                                           }
                                                                                      }
                                                                              }
                                                    }
                                     };

            var task = Task.Raise("Test task for completion 1", assignedTo, taskType, assignedTo);
            var validationMessages = task.ValidateComplete("Completion message 1", completingUser);
            Assert.That(validationMessages.Count, Is.EqualTo(0));
        }
    }
}
