using lofi_backend.Controllers;
using lofi_backend.Data_Models;
using lofi_backend.Repository;
using lofi_backend.Service;
using Microsoft.AspNetCore.Http;
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
        public void GetUser_ReturnsUser()
        {
            var expectedUser = new User(id: 1, username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", age: 30);
            _mockService.Setup(service => service.GetUser(1)).Returns(expectedUser);

            var result = _userController.GetUser(1) as ObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            result.Value.ShouldBe(expectedUser);
        }

        [Test]
        public void GetUser_ReturnsNotFound()
        {
            _mockService.Setup(service => service.GetUser(1)).Throws(new Exception());

            var result = _userController.GetUser(1) as NotFoundResult;

            Console.WriteLine(result.StatusCode);
            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
        }

        [Test]
        public void CreateUser_ReturnsCreatedUser()
        {
            var userToCreate = new User(id: 1, username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", age: 30);
            _mockService.Setup(service => service.CreateUser(userToCreate)).Returns(userToCreate);

            var result = _userController.CreateUser(userToCreate) as ObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status201Created);
            result.Value.ShouldBe(userToCreate);
        }

        [Test]
        public void CreateUser_UserExists()
        {
            var userToCreate = new User(id: 1, username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", age: 30);
            _mockService.Setup(service => service.CreateUser(userToCreate)).Returns(value: null);

            var result = _userController.CreateUser(userToCreate) as BadRequestResult;

            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
            result.Value.ShouldBeNull();
        }

        [Test]
        public void EditUser_ReturnsUpdatedUser()
        {
            var updatedUser = new User(id: 1, username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", age: 30);

            _mockService.Setup(service => service.EditUser(updatedUser)).Returns(updatedUser);

            var result = _userController.EditUser(updatedUser) as ObjectResult;

            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
            result.Value.ShouldBe(updatedUser);
        }

        [Test]
        public void EditUser_UserDoesNotExist()
        {
            var updatedUser = new User(id: 1, username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", age: 30);
            _mockService.Setup(service => service.EditUser(updatedUser)).Returns(value: null);

            var result = _userController.EditUser(updatedUser) as ObjectResult;
            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
            result.Value.ShouldBeNull();
        }
        [Test]
        public void DeleteUser_UserExists()
        {
            _mockService.Setup(service => service.RemoveUser(1));

            var result = _userController.RemoveUser(1) as ObjectResult;
            
            result.StatusCode.ShouldBe(StatusCodes.Status204NoContent);
        }

        [Test]
        public void DeleteUser_UserDoesNotExist()
        {
            _mockService.Setup(service => service.RemoveUser(1));
            var result = _userController.RemoveUser(1) as ObjectResult;
            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
        }
    }
}