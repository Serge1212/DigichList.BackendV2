using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using System;
using Xunit;

namespace DigicnList.Backend.Tests.ViewModels
{
    public class DefectViewModelTests
    {
        [Fact]
        public void ToViewModel_ShouldMapModel_Correctly()
        {
            // Arrange.
            var expectedCreatedAt = DateTime.Now.AddHours(-1);
            var expectedChangedAt = DateTime.Now;
            var expectedAssignee = "First Last, username";
            var expectedStatus = "Opened";

            var defect = new Defect
            {
                Id = 1,
                Description = "test desc",
                CreatedAt = expectedCreatedAt,
                RoomNumber = 1,
                AssignedWorker = new User
                {
                    Id = 1,
                    FirstName = "First",
                    LastName = "Last",
                    Username = "username",
                },
                CreatedBy = "created by test",
                Status = 1,
                StatusChangedAt = expectedChangedAt,
            };

            // Act.
            var result = DefectViewModel.ToViewModel(defect);

            // Assert.
            Assert.Equal(defect.Id, result.Id);
            Assert.Equal(defect.Description, result.Description);
            Assert.Equal(expectedCreatedAt.ToShortDateString(), result.CreatedAt);
            Assert.Equal(defect.RoomNumber, result.RoomNumber);
            Assert.Equal(expectedAssignee, result.Assignee);
            Assert.Equal(defect.AssignedWorker.Id, result.AssigneeId);
            Assert.Equal(defect.CreatedBy, result.Publisher);
            Assert.Equal(expectedStatus, result.Status);
            Assert.Equal(expectedChangedAt.ToShortDateString(), result.StatusChangedAt);
        }
    }
}
