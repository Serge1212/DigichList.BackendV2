using DigichList.Backend.Controllers;
using DigichList.Backend.Helpers;
using DigichList.Backend.Interfaces;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace DigicnList.Backend.Tests.Controllers
{
    public class AdminControllerTests
    {
        private readonly AdminController _adminController;
        private readonly Mock<IAdminService> _adminServiceMock;
        private readonly Mock<JwtService> _jwtServiceMock;

        public AdminControllerTests()
        {
            _adminServiceMock = new Mock<IAdminService>();
            _jwtServiceMock = new Mock<JwtService>();
            _adminController = new AdminController(_adminServiceMock.Object, _jwtServiceMock.Object);
        }

        [Fact]
        public async Task GetAdmins_ReturnsOkResult()
        {
            // Arrange.
            var admins = new[] { new AdminViewModel(), new AdminViewModel() };
            _adminServiceMock
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(admins);

            // Act.
            var result = await _adminController.GetAdmins();

            // Assert.
            Assert.IsType<OkObjectResult>(result);

            var actualAdmins = result as OkObjectResult;
            Assert.Equal(admins, actualAdmins.Value);
        }
    }
}
