using lofi_backend.Controllers;
using lofi_backend.Data_Models;
using lofi_backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;

namespace Testing.Projects;

public class ControllerTesting
{
    private Mock<IProjectService> _projectServiceMock;
    private ProjectsController _projectsController;
    [SetUp]
    public void Setup()
    {
        _projectServiceMock = new Mock<IProjectService>();
        _projectsController = new ProjectsController(_projectServiceMock.Object);
    }

    [Test]
    public void GetAllProjects_ReturnsAllProjects()
    {
        var projectList = new List<Project>
        {
            new Project
            {
                Id = 1,
                Name = "Website Redesign",
                StartDate = new DateTime(2026, 1, 15),
                EndDate = new DateTime(2026, 4, 30),
                Timers = new List<TaskTimer>(),
                UserId = 101
            },
            new Project
            {
                Id = 2,
                Name = "Mobile App Development",
                StartDate = new DateTime(2026, 3, 1),
                EndDate = new DateTime(2026, 9, 15),
                Timers = new List<TaskTimer>(),
                UserId = 101
            },
            new Project
            {
                Id = 3,
                Name = "Data Migration Strategy",
                StartDate = new DateTime(2026, 5, 20),
                EndDate = new DateTime(2026, 7, 1),
                Timers = new List<TaskTimer>(),
                UserId = 102
            }
        };
        _projectServiceMock.Setup(service => service.GetAllProjects()).Returns(projectList);
        var result = _projectsController.GetAllProjects() as ObjectResult;
        result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        result.Value.ShouldBe(projectList);
    }

    [Test]
    public void GetProject_ShouldReturnProjectById()
    {
        var project = new Project
        {
            Id = 1,
            Name = "Website Redesign",
            StartDate = new DateTime(2026, 1, 15),
            EndDate = new DateTime(2026, 4, 30),
            Timers = new List<TaskTimer>(),
            UserId = 101
        };

        _projectServiceMock.Setup(service => service.GetProject(1)).Returns(project);
        var result = _projectsController.GetProject(1) as ObjectResult;
        result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        result.Value.ShouldBe(project);
    }

    [Test]
    public async Task CreateProject_ShouldReturnCreatedProject()
    {
        var project = new Project
        {
            Id = 1,
            Name = "Website Redesign",
            StartDate = new DateTime(2026, 1, 15),
            EndDate = new DateTime(2026, 4, 30),
            Timers = new List<TaskTimer>(),
            UserId = 101
        };
        _projectServiceMock.Setup(service => service.CreateProject(project)).ReturnsAsync(project);

        var result = await _projectsController.CreateProject(project) as ObjectResult;

        result.StatusCode.ShouldBe(StatusCodes.Status201Created);
        result.Value.ShouldBe(project);

    }
    //test needed for delete method
    }
