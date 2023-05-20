using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using Xunit;

namespace DigicnList.Backend.Tests.ViewModels
{
    public class UserViewModelTests
    {
        [Fact]
        public void ToViewModel_ShouldMapModel_Correctly()
        {
            // Arrange.
            var user = new User
            {
                Id = 1,
                ChatId = 1,
                FirstName = "First",
                LastName = "Last",
                Username = "username",
                IsRegistered = true,
                RoleId = 1,
                Role = new Role
                {
                    Id = 1,
                    Name = "Test role",
                    CanAdd = true,
                    CanBeAssigned = true,
                },
                Defects = null,
            };

            // Act.
            var result = UserViewModel.ToViewModel(user);

            // Assert.
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.FirstName, result.FirstName);
            Assert.Equal(user.LastName, result.LastName);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.IsRegistered, result.IsRegistered);
            Assert.Equal(user.Role.Name, result.RoleName);
            Assert.Equal(user.Role.Id, result.RoleId);
        }
    }
}
