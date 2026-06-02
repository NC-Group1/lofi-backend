using lofi_backend.Controllers;
using lofi_backend.Data_Models;
using lofi_backend.Repository;
using lofi_backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;

namespace Testing.Users
{
    public class Tests
    {
        private Mock<IUserService> _mockService;
        private UsersController _userController;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IUserService>();
            _userController = new UsersController(_mockService.Object);
        }

        [Test]
        public async Task GetUser_ReturnsUser()
        {
            var expectedUser = new UserData(
                id: "1", username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", dateOfBirth: DateTime.Now, gender: 0);
            var authUser = new AuthenticatedUser(expectedUser, new AuthToken("", "", "", "", "", ""));
            _mockService.Setup(service => service.GetUserAsync(1.ToString(), "")).ReturnsAsync(authUser);

            var result = await _userController.GetUserAsync("1", "123456") as ObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }

        [Test]
        public async Task GetUser_ReturnsNotFound()
        {
            _mockService.Setup(service => service.GetUserAsync(1.ToString(), "")).Throws(new Exception());

            var result = await _userController.GetUserAsync("1", "") as NotFoundResult;

            Console.WriteLine(result.StatusCode);
            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
        }

        [Test]
        public async Task CreateUser_ReturnsCreatedUser()
        {
            var userToCreate =new UserWithPassword(new UserData(id: "1", username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", dateOfBirth: DateTime.Now, gender: 0), "");
            var authUser = new AuthenticatedUser(userToCreate.UserData, new AuthToken("", "", "", "", "", ""));
            _mockService.Setup(service => service.CreateUser(userToCreate)).ReturnsAsync(authUser);

            var result = await _userController.CreateUserAsync(userToCreate) as ObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            result.Value.ShouldBe(authUser);
        }

        [Test]
        public async Task CreateUser_UserExists()
        {
            var userToCreate =new UserWithPassword(new UserData(id: "1", username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", dateOfBirth: DateTime.Now, gender: 0), "");
            var authUser = new AuthenticatedUser(userToCreate.UserData, new AuthToken("", "", "", "", "", ""));
            _mockService.Setup(service => service.CreateUser(userToCreate)).ThrowsAsync(new Exception());

            var result = await _userController.CreateUserAsync(userToCreate) as ObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
            result.ShouldBeOfType<BadRequestObjectResult>();

        }

        [Test]
        public void EditUser_ReturnsUpdatedUser()
        {
            var updatedUser = new UserData(id: "1", username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", dateOfBirth: DateTime.Now, gender: 0);

            _mockService.Setup(service => service.EditUser(updatedUser)).Returns(updatedUser);

            var result = _userController.EditUser(updatedUser);

            result.ShouldBeOfType<OkObjectResult>();

        }

        [Test]
        public void EditUser_UserDoesNotExist()
        {
            var updatedUser = new UserData(id: "1", username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", dateOfBirth: DateTime.Now, gender: 0);
            _mockService.Setup(service => service.EditUser(updatedUser)).Throws(new Exception());

            var result = _userController.EditUser(updatedUser);
            result.ShouldBeOfType<BadRequestObjectResult>();

        }
        [Test]
        public void DeleteUser_UserExists()
        {
            var deletedUser = new UserData(id: "1", username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", dateOfBirth: DateTime.Now, gender: 0);

            _mockService.Setup(service => service.RemoveUser(1.ToString())).Returns(deletedUser);

            var result = _userController.RemoveUser("1");

            result.ShouldBeOfType<NoContentResult>();
        }

        [Test]
        public void DeleteUser_UserDoesNotExist()
        {
            _mockService.Setup(service => service.RemoveUser(1.ToString()));
            var result = _userController.RemoveUser("1") as ObjectResult;
            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
        }
    }
}