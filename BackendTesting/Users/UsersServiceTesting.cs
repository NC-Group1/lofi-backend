using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using lofi_backend.Repository;
using lofi_backend.Service;
using lofi_backend.Data_Models;
using lofi_backend.Repository.Authentication;
using Shouldly;

namespace Testing.UsersServiceTesting
{
    internal class UsersServiceTesting
    {
        private Mock<IUserRepository> _mockRepo;
        private Mock<IAuthenticationRepository> _mockAuth;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            _mockRepo = new Mock<IUserRepository>();
            _mockAuth = new Mock<IAuthenticationRepository>();
            _userService = new UserService(_mockRepo.Object, _mockAuth.Object);
        }

        [Test]
        public async Task GetUser_ReturnsUser()
        {
            // Arrange
            var user = new UserData(id: "1", username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", dateOfBirth: DateTime.Now, gender: 0);
            var authUser = new AuthenticatedUser(user, new CookieOptions());
            _mockRepo.Setup(repo => repo.FetchUser("1")).Returns(user);
            // Act
            var result = await _userService.GetUserAsync("1", "");
            // Assert
            result.UserData.ShouldBe(user);
        }

        [Test]
        public async Task CreateUser_ReturnsCreatedUser()
        {
            // Arrange
            var user = new UserData(id: "1", username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", dateOfBirth: DateTime.Now, gender: 0);

            _mockAuth.Setup(repo => repo.SignUpAsync(user.Email, "")).ReturnsAsync(new CookieOptions("1", "", "", "", "", ""));
            _mockRepo.Setup(repo => repo.InsertUser(user)).Returns(user);
            var result = await _userService.CreateUser(new UserWithPassword(user, ""));

            result.UserData.ShouldBe(user);
        }

        [Test]
        public void EditUser_ReturnsUpdatedUser()
        {
            // Arrange
            var updatedUser = new UserData(id: "1", username: "Updated User", firstName: "John", lastName: "Music", email: "email@email.com", dateOfBirth: DateTime.Now, gender: 0);
            _mockRepo.Setup(repo => repo.UpdateUser(updatedUser)).Returns(updatedUser);

            var result = _userService.EditUser(updatedUser);

            Assert.That(result, Is.EqualTo(updatedUser));
        }

        [Test]
        public void DeleteUser_CallsRepositoryDelete()
        {
            // Arrange
            var userId = "1";
            _mockRepo.Setup(repo => repo.DeleteUser(userId));
            // Act
            _userService.RemoveUser(userId);
            // Assert
            _mockRepo.Verify(repo => repo.DeleteUser(userId), Times.Once);
        }
    }
}
