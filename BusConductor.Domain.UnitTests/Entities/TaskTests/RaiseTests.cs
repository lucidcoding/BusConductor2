using System;
using NUnit.Framework;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.Enumerations;

namespace BusConductor.Domain.UnitTests.Entities.TaskTests
{
    [TestFixture]
    public class RaiseTests
    {
        [Test]
        public void Given_a_valid_set_of_parameters_is_provided_When_I_call_Raise_Then_a_Task_is_returned()
        {
            const string description = "Test Description 1";
            var assignedTo = new User { Id = Guid.NewGuid() };

            var taskType = new TaskType
            {
                Id = Guid.NewGuid(),
                ServiceLevelAgreementMinutes = 30,
                WarningWindowMinutes = 10,
            };

            var currentUser = new User { Id = Guid.NewGuid() };

            var task = Task.Raise(description, assignedTo, taskType, currentUser);
            Assert.That(task, Is.Not.Null);
            Assert.That(task.Description, Is.EqualTo(description));
            Assert.That(task.AssignedTo, Is.EqualTo(assignedTo));
            Assert.That(task.Type, Is.EqualTo(taskType));
            Assert.That(task.Status, Is.EqualTo(TaskStatus.InProgress));
            Assert.That(task.CompletedOn, Is.Not.EqualTo(default(DateTime)));
            Assert.That(task.CreatedBy, Is.EqualTo(currentUser));
            Assert.That(task.DueDate, Is.EqualTo(task.CreatedOn.AddMinutes(30)));
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public void Given_an_invalid_set_of_parameters_When_I_call_Raise_Then_exception_is_thrown()
        {
            Task.Raise(null, null, null, null);
        }
    }
}
