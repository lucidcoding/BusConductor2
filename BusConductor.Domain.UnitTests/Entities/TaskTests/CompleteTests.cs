using NUnit.Framework;
using BusConductor.Domain.Entities;
using BusConductor.Domain.Enumerations;

namespace BusConductor.Domain.UnitTests.Entities.TaskTests
{
    [TestFixture]
    public class CompleteTests
    {
        [Test]
        public void Given_I_supply_valid_parameters_When_assigned_user_Completes_task_Then_Task_is_completed_as_expected()
        {
            var assignedTo = new User();
            var taskType = new TaskType { WarningWindowMinutes = 10 };
            var currentUser = new User();
            var task = Task.Raise("Test task for completion 1", assignedTo, taskType, currentUser);
            task.Complete("Completion message 1", assignedTo);
            Assert.That(task.Status, Is.EqualTo(TaskStatus.Completed));
            Assert.That(task.CompletedOn, Is.Not.Null);
        }
    }
}
