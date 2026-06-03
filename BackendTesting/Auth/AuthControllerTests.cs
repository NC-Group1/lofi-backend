//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using lofi_backend.Controllers;
//using lofi_backend.Data_Models;
//using lofi_backend.Service;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using Shouldly;
//using Supabase.Interfaces;

//namespace Testing.Auth
//{
//    internal class AuthControllerTests
//    {
//        private Mock<ISupabaseClient<TUser, Tsess>> _mockClient;
//        private AuthController _authController;

//        [SetUp]
//        public void SetUp()
//        {
//            _mockClient = new Mock<ISupabaseClient>();
//            _authController = new AuthController(_mockClient.Object);
//        }

//        [Test]
//        public async Task SignIn_ReturnsAuthUser()
//        {
//            var expectedUser = new UserData(id: "1", username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", dateOfBirth: DateTime.Now, gender: 0);
//            var authUser = new UserWithPassword(expectedUser, "password");
//            _mockClient.Setup(service => service.SignIn("email", "password")).Returns(new AuthenticatedUser());

//            var result = await _authController.SignIn("email", "password") as ObjectResult;

//            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
//        }

//        [Test]
//        public async Task SignIn_ReturnsNotFound()
//        {
//            _mockClient.Setup(service => service.SignIn("username", "password")).Throws(new Exception());

//            var result = await _authController.SignIn("username", "password") as ObjectResult;

//            Console.WriteLine(result?.StatusCode);
//            result?.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
//        }

//        [Test]
//        public async Task SignUp_ReturnsAuthUser()
//        {
//            var userToCreate =new UserWithPassword(new UserData(id: "1", username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", dateOfBirth: DateTime.Now, gender: 0), "");
//            var authUser = new AuthenticatedUser(userToCreate.UserData, new lofi_backend.Data_Models.CookieOptions("", "", "", "", "", ""));
//            _mockClient.Setup(service => service.SignUp(userToCreate)).Returns(authUser);

//            var result = await _authController.SignUp(userToCreate) as ObjectResult;

//            result?.StatusCode.ShouldBe(StatusCodes.Status200OK);
//            result?.Value.ShouldBe(authUser);
//        }

//        [Test]
//        public async Task SignUp_UserAlreadyExists()
//        {
//            var userToCreate = new UserWithPassword(new UserData(id: "1", username: "Test User", firstName: "John", lastName: "Music", email: "email@email.com", dateOfBirth: DateTime.Now, gender: 0), "");
//            var authUser = new AuthenticatedUser(userToCreate.UserData, new lofi_backend.Data_Models.CookieOptions("", "", "", "", "", ""));
//            _mockClient.Setup(service => service.SignUp(userToCreate)).Throws(new Exception());

//            var result = await _authController.SignUp(userToCreate) as ObjectResult;

//            result?.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
//        }

//    }
//}
