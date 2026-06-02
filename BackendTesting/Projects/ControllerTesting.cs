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
                UserId = "101"
            },
            new Project
            {
                Id = 2,
                Name = "Mobile App Development",
                StartDate = new DateTime(2026, 3, 1),
                EndDate = new DateTime(2026, 9, 15),
                Timers = new List<TaskTimer>(),
                UserId = "1" 
            },
            new Project
            {
                Id = 3,
                Name = "Data Migration Strategy",
                StartDate = new DateTime(2026, 5, 20),
                EndDate = new DateTime(2026, 7, 1),
                Timers = new List<TaskTimer>(),
                UserId = "102"
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
            UserId = "101"
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
            UserId = "101"
        };
        _projectServiceMock.Setup(service => service.CreateProject(project)).ReturnsAsync(project);

        var result = await _projectsController.CreateProject(project) as ObjectResult;

        result.StatusCode.ShouldBe(StatusCodes.Status201Created);
        result.Value.ShouldBe(project);

    }

    [Test]
    public async Task DeleteProject_ShouldDeleteProject()
    {
        var project = new Project
        {
            Id = 1,
            Name = "Website Redesign",
            StartDate = new DateTime(2026, 1, 15),
            EndDate = new DateTime(2026, 4, 30),
            Timers = new List<TaskTimer>(),
            UserId = "101"
        };
        _projectServiceMock.Setup(service => service.DeleteProject(1)).ReturnsAsync(project);
        var result = await _projectsController.DeleteProject(1) as ObjectResult;

        result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        result.Value.ShouldBe(project);
    }

    [Test]
    public async Task DeleteProject_ReturnsBadRequest_WhenIdIsZero()
    {
        var result = await _projectsController.DeleteProject(0) as ObjectResult;

        result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        result.ShouldBeOfType<BadRequestObjectResult>();
    }

    [Test]
    public async Task DeleteProject_ReturnsNotFound_WhenProjectDoesNotExist()
    {

        _projectServiceMock.Setup(service => service.DeleteProject(999)).ReturnsAsync((Project)null);

        var result = await _projectsController.DeleteProject(999) as ObjectResult;

        result.StatusCode.ShouldBe(StatusCodes.Status404NotFound);
        result.ShouldBeOfType<NotFoundObjectResult>();
    }

    [Test]
    public async Task EditProject_ReturnsUpdatedProject()
    {
        var project = new Project
        {
            Id = 1,
            Name = "Website Redesign",
            StartDate = new DateTime(2026, 1, 15),
            EndDate = new DateTime(2026, 4, 30),
            Timers = new List<TaskTimer>(),
            UserId = "101"
        };
        var updatedProject = new Project
        {
            Id = 1,
            Name = "Mobile App Development",
            StartDate = new DateTime(2026, 3, 1),
            EndDate = new DateTime(2026, 9, 15),
            Timers = new List<TaskTimer>(),
            UserId = "101"
        };

        _projectServiceMock.Setup(service => service.EditProject(updatedProject)).ReturnsAsync(updatedProject);

        var result = await _projectsController.EditProject(updatedProject) as ObjectResult;

        result.StatusCode.ShouldBe(StatusCodes.Status200OK);
        result.ShouldBeOfType<OkObjectResult>();

    }

    [Test]
    public async Task EditProject_ProjectDoesNotExist()
    {
        var updatedProject = new Project
        {
            Id = 1,
            Name = "Mobile App Development",
            StartDate = new DateTime(2026, 3, 1),
            EndDate = new DateTime(2026, 9, 15),
            Timers = new List<TaskTimer>(),
            UserId = "101"
        };
        _projectServiceMock.Setup(service => service.EditProject(updatedProject)).Throws(new Exception());

        var result = await _projectsController.EditProject(updatedProject) as ObjectResult;
        result.StatusCode.ShouldBe(StatusCodes.Status400BadRequest);
        result.ShouldBeOfType<BadRequestObjectResult>();

    }
}
