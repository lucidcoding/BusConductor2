using System.Linq;
using NUnit.Framework;
using BusConductor.Domain.Entities;

namespace BusConductor.Domain.UnitTests.Entities.TaskTests
{
    [TestFixture]
    public class ValidateRaiseTests
    {
        [Test]
        public void Given_a_valid_set_of_parameters_is_provided_When_I_call_ValidateRaise_Then_no_messages_are_returned()
        {
            var assignedTo = new User();
            var taskType = new TaskType{ WarningWindowMinutes = 10 };
            var currentUser = new User();
            var validationMessages = Task.ValidateRaise("Test Description 1", assignedTo, taskType, currentUser);
            Assert.That(validationMessages.Count, Is.EqualTo(0));
        }

        //Common wisdom suggests that maybe we should have a separate test for each validation error, but is this really the most
        //pragmatic way to do it? We need to ensure developers actually do the trests and they might not bother if we enforce rules
        //that increase workload.
        [Test]
        public void Given_an_invalid_set_of_parameters_When_I_call_ValidateRaise_Then_correct_messages_are_returned()
        {
            var validationMessages = Task.ValidateRaise(null, null, null, null);
            Assert.That(validationMessages.Count, Is.EqualTo(3));
            Assert.That(validationMessages.Select(x => x.Text).Contains("Description is null or empty."));
            Assert.That(validationMessages.Select(x => x.Text).Contains("AssignedTo is null."));
            Assert.That(validationMessages.Select(x => x.Text).Contains("Type is null."));
        }
    }
}
