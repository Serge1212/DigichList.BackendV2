using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using Xunit;

namespace DigicnList.Backend.Tests.ViewModels
{
    public class RoleViewModelTests
    {
        [Fact]
        public void ToViewModel_ShouldMapModel_Correctly()
        {
            // Arrange.
            var role = new Role
            {
                Id = 1,
                Name = "Test role",
            };

            // Act.
            var result = RoleViewModel.ToViewModel(role);

            // Assert.
            Assert.Equal(role.Id, result.Id);
            Assert.Equal(role.Name, result.Name);
        }
    }
}
