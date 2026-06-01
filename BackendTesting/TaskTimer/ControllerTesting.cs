using lofi_backend.Controllers;
using lofi_backend.Data_Models;
using lofi_backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Moq;

namespace Testing;

public class ControllerTesting
{
    private Mock<ITaskTimerService> _mockService;
    private TaskTimersController _taskTimerController;
    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<ITaskTimerService>();
        _taskTimerController = new TaskTimersController(_mockService.Object);
    }

    [Test]
    public void GetTimerByTimerId_ShouldReturnOK()
    {
        var expectedTimer = new TaskTimer
        {
            Id = 1,
            DateCreated = new DateTime(2026, 6, 1, 9, 0, 0),
            DateUpdated = new DateTime(2026, 6, 1, 10, 30, 0),
            Duration = 5400, // 1 hour 30 minutes
            IsActive = false,
            ProjectId = 1
        };

        _mockService.Setup(service => service.GetTimerByTimerId(1)).Returns(expectedTimer);

        var result = _taskTimerController.GetTimerByTimerId(1) as ObjectResult;

        result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        result.ShouldBeOfType<OkObjectResult>();
    }
    [Test]
    public async Task CreateNewTimer_ShouldReturnCreated201()
    {
        var addNewtaskTimer = new TaskTimer
        {
            Id = 1,
            DateCreated = new DateTime(2026, 6, 1, 9, 0, 0),
            DateUpdated = new DateTime(2026, 6, 1, 10, 30, 0),
            Duration = 5400, // 1 hour 30 minutes
            IsActive = false,
            ProjectId = 1
        };
        _mockService.Setup(service => service.CreateNewTimer(addNewtaskTimer)).ReturnsAsync(addNewtaskTimer);
        var result = await _taskTimerController.CreateNewTimer(addNewtaskTimer) as ObjectResult;

        result.StatusCode.ShouldBe(StatusCodes.Status200OK);

    }

}
