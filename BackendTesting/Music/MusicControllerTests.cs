using lofi_backend.Controllers;
using lofi_backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Music
{
    internal class MusicControllerTests
    {
        private Mock<IMusicService> _mockService;
        private MusicController _musicController;

        [SetUp]
        public void SetUp()
        {
            _mockService = new Mock<IMusicService>();
            _musicController = new MusicController(_mockService.Object);
        }

        [Test]
        public void GetAllMusics_Returns200Ok()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllMusics()).Returns(new List<lofi_backend.Data_Models.Music>());
            // Act
            var result = _musicController.GetAllMusics() as OkObjectResult;
            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }

        [Test]
        public void GetAllMusics_Returns404NotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllMusics()).Throws(new Exception());
            // Act
            var result = _musicController.GetAllMusics() as NotFoundResult;
            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
        }

        [Test]
        public void GetMusicById_Returns200Ok()
        {
            // Arrange
            _mockService.Setup(service => service.GetMusicById(1)).Returns(new lofi_backend.Data_Models.Music());
            // Act
            var result = _musicController.GetMusicById(1) as OkObjectResult;
            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }

        [Test]
        public void GetMusicById_Returns404NotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetMusicById(1)).Throws(new Exception());
            // Act
            var result = _musicController.GetMusicById(1) as NotFoundResult;
            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
        }

        [Test]
        public void CreateMusic_ReturnsCreatedAtAction_WhenMusicIsValid()
        {
            // Arrange
            var music = new lofi_backend.Data_Models.Music
            {
                Id = 1,
                Title = "Test Music",
                Artist = "Test Artist",
                Channel = "Test Channel",
                Mood = lofi_backend.Data_Models.Enums.Mood.Romantic,
                Genre = lofi_backend.Data_Models.Enums.Genre.LoFi,
                URL = "Test URL"
            };
            _mockService.Setup(service => service.CreateMusic(music)).Returns(music);
            // Act
            var result = _musicController.CreateMusic(music) as OkObjectResult;
            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        }

        [Test]
        public void CreateMusic_ReturnsBadRequest_WhenMusicIsInvalid()
        {
            // Arrange
            var music = new lofi_backend.Data_Models.Music
            {
                Id = 1,
                Title = "Test Music",
                Artist = "Test Artist",
                Channel = "Test Channel",
                Mood = lofi_backend.Data_Models.Enums.Mood.Romantic,
                Genre = lofi_backend.Data_Models.Enums.Genre.LoFi,
                URL = "Test URL"
            };
            _mockService.Setup(service => service.CreateMusic(music)).Throws(new Exception());
            // Act
            var result = _musicController.CreateMusic(music) as BadRequestResult;
            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }

       
        [Test]
        public void CreateMusic_MusicAlreadyExists_Returns400BadRequest()
        {
            // Arrange
            var music = new lofi_backend.Data_Models.Music
            {
                Id = 1,
                Title = "Test Music",
                Artist = "Test Artist",
                Channel = "Test Channel",
                Mood = lofi_backend.Data_Models.Enums.Mood.Romantic,
                Genre = lofi_backend.Data_Models.Enums.Genre.LoFi,
                URL = "Test URL"
            };
            _mockService.Setup(service => service.CreateMusic(music)).Throws(new Exception());
            // Act
            var result = _musicController.CreateMusic(music) as BadRequestResult;
            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        }
    }


    }
