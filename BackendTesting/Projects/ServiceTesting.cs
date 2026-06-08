using lofi_backend.Models;
using lofi_backend.Repository;
using lofi_backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;

namespace Testing.Projects;

internal class ServiceTesting
{
    private Mock<IProjectRepository> _mockRepo;
    private ProjectService _projectService;

    [SetUp]
    public void SetUp()
    {
        _mockRepo = new Mock<IProjectRepository>();
        _projectService = new ProjectService(_mockRepo.Object);
    }

    [Test]
    public void GetAllProjects_ShouldReturnAllProjects()
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
                UserId = "101"
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
        _mockRepo.Setup(repo => repo.GetAllProjects()).Returns(projectList);
        var result = _projectService.GetAllProjects();

        Assert.That(result, Is.EquivalentTo(projectList));
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
        _mockRepo.Setup(repo => repo.GetProject(1)).Returns(project);
        var result = _projectService.GetProject(1);
        Assert.That(result, Is.EqualTo(project));
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
        _mockRepo.Setup(repo => repo.CreateProject(project)).ReturnsAsync(project);
        var result = await _projectService.CreateProject(project);

        Assert.That(result, Is.EqualTo(project));
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
        _mockRepo.Setup(repo => repo.DeleteProject(1)).ReturnsAsync(project);
        var result = await _projectService.DeleteProject(1);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
    }

    [Test]

    public async Task EditProject_ShouldReturnUpdatedProject()
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

        _mockRepo.Setup(repo => repo.EditProject(updatedProject)).ReturnsAsync(updatedProject);

        var result = await _projectService.EditProject(updatedProject);

        Assert.That(result, Is.EqualTo(updatedProject));
    }
}


