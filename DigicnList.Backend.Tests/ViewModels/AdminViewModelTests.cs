using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using DigichList.Core.Enums;
using Xunit;

namespace DigicnList.Backend.Tests.ViewModels
{
    public class AdminViewModelTests
    {
        [Theory]
        [InlineData("Admin", AccessLevels.Admin)]
        [InlineData("Superadmin", AccessLevels.SuperAdmin)]
        public void ToViewModel_ShouldMapModel_Correctly(string expectedAccessLevel, AccessLevels accessLevel)
        {
            // Arrange.
            var admin = new Admin
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Test 2",
                Email = "test@email",
                AccessLevel = accessLevel,
            };

            // Act.
            var result = AdminViewModel.ToViewModel(admin);

            // Assert.
            Assert.Equal(admin.Id, result.Id);
            Assert.Equal(admin.FirstName, result.FirstName);
            Assert.Equal(admin.LastName, result.LastName);
            Assert.Equal(admin.Email, result.Email);
            Assert.Equal(expectedAccessLevel, result.AccessLevel);
        }
    }
}
