using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using lofi_backend.Repository;
using lofi_backend.Service;
using lofi_backend.Data_Models;

namespace Testing.Users
{
    internal class ServiceTesting
    {
        private Mock<IUserRepository> _mockRepo;
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            _mockRepo = new Mock<IUserRepository>();
            _userService = new UserService(_mockRepo.Object);
        }

        [Test]
        public void GetUser_ReturnsUser()
        {
            // Arrange
            var user = new User(id: 1, username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", age: 30);
            _mockRepo.Setup(repo => repo.FetchUser(1)).Returns(user);
            // Act
            var result = _userService.GetUser(1);
            // Assert
            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void CreateUser_ReturnsCreatedUser()
        {
            // Arrange
            var user = new User(id: 1, username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", age: 30);
            _mockRepo.Setup(repo => repo.InsertUser(user)).Returns(user);

            var result = _userService.CreateUser(user);

            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public void EditUser_ReturnsUpdatedUser()
        {
            // Arrange
            var user = new User(id: 1, username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", age: 30);
            var updatedUser = new User(id: 1, username: "Updated User", firstName: "John", lastName: "Music", email: "email@email.com", age: 30);
            _mockRepo.Setup(repo => repo.UpdateUser(updatedUser)).Returns(updatedUser);

            var result = _userService.EditUser(updatedUser);

            Assert.That(result, Is.EqualTo(updatedUser));
        }

        [Test]
        public void DeleteUser_CallsRepositoryDelete()
        {
            // Arrange
            var userId = 1;
            _mockRepo.Setup(repo => repo.DeleteUser(userId));
            // Act
            _userService.RemoveUser(userId);
            // Assert
            _mockRepo.Verify(repo => repo.DeleteUser(userId), Times.Once);
        }
    }
}
