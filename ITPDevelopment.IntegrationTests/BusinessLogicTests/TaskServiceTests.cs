#nullable disable

using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ITPDevelopment.IntegrationTests.BusinessLogicTests;

public class TaskServiceTests : IntegrationTestBase
{
    [Fact]
    public async Task GetTaskListAsync_Success()
    {
        var taskName = Guid.NewGuid().ToString();
        var description = Guid.NewGuid().ToString();
        var projectName = Guid.NewGuid().ToString();
        var createProjectResult = await ProjectService.CreateProjectAsync(projectName);
        for (int i = 0; i < 5; i++)
        {
            await TaskService.CreateTaskAsync(taskName, description, createProjectResult.Response.Id);
        }

        var firstGetTaskListResult = await TaskService.GetTaskListAsync(0, 3);
        var secondGetTaskListResult = await TaskService.GetTaskListAsync(3, 3);

        firstGetTaskListResult.Response.Count().Should().Be(3);
        secondGetTaskListResult.Response.Count().Should().Be(2);
    }
    
    [Fact]
    public async Task CreateTaskAsync_Success()
    {
        var taskName = Guid.NewGuid().ToString();
        var description = Guid.NewGuid().ToString();
        var projectName = Guid.NewGuid().ToString();
        var createProjectResult = await ProjectService.CreateProjectAsync(projectName);

        for (int i = 0; i < 5; i++)
        {
            await TaskService.CreateTaskAsync(taskName, description, createProjectResult.Response.Id);
        }

        var taskCount = await DatabaseContextFixture.TaskEntities.CountAsync();
        taskCount.Should().Be(5);
    }
    
    [Fact]
    public async Task StartTaskAsync_Success()
    {
        var taskName = Guid.NewGuid().ToString();
        var description = Guid.NewGuid().ToString();
        var projectName = Guid.NewGuid().ToString();
        var createProjectResult = await ProjectService.CreateProjectAsync(projectName);
        var createTaskResult = await TaskService.CreateTaskAsync(taskName, description, createProjectResult.Response.Id);

        await TaskService.StartTaskAsync(createTaskResult.Response.Id);

        var task = await DatabaseContextFixture.TaskEntities.FirstOrDefaultAsync(x => x.Id == createTaskResult.Response.Id);
        task.StartDate.Should().NotBeNull();
    }
    
    [Fact]
    public async Task StartTaskAsync_ThrowTaskNotFound()
    {
        var startTaskResult = await TaskService.StartTaskAsync(Guid.NewGuid());

        startTaskResult.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public async Task StartTaskAsync_ThrowDateHasAlreadyBeenSet()
    {
        var taskName = Guid.NewGuid().ToString();
        var description = Guid.NewGuid().ToString();
        var projectName = Guid.NewGuid().ToString();
        var createProjectResult = await ProjectService.CreateProjectAsync(projectName);
        var createTaskResult = await TaskService.CreateTaskAsync(taskName, description, createProjectResult.Response.Id);
        await TaskService.StartTaskAsync(createTaskResult.Response.Id);
        
        var startTaskResult = await TaskService.StartTaskAsync(Guid.NewGuid());

        startTaskResult.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public async Task EndTaskAsync_Success()
    {
        var taskName = Guid.NewGuid().ToString();
        var description = Guid.NewGuid().ToString();
        var projectName = Guid.NewGuid().ToString();
        var createProjectResult = await ProjectService.CreateProjectAsync(projectName);
        var createTaskResult = await TaskService.CreateTaskAsync(taskName, description, createProjectResult.Response.Id);

        await TaskService.EndTaskAsync(createTaskResult.Response.Id);

        var task = await DatabaseContextFixture.TaskEntities.FirstOrDefaultAsync(x => x.Id == createTaskResult.Response.Id);
        task.EndDate.Should().NotBeNull();
    }
    
    [Fact]
    public async Task EndTaskAsync_ThrowTaskNotFound()
    {
        var startTaskResult = await TaskService.EndTaskAsync(Guid.NewGuid());

        startTaskResult.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public async Task EndTaskAsync_ThrowDateHasAlreadyBeenSet()
    {
        var taskName = Guid.NewGuid().ToString();
        var description = Guid.NewGuid().ToString();
        var projectName = Guid.NewGuid().ToString();
        var createProjectResult = await ProjectService.CreateProjectAsync(projectName);
        var createTaskResult = await TaskService.CreateTaskAsync(taskName, description, createProjectResult.Response.Id);
        await TaskService.EndTaskAsync(createTaskResult.Response.Id);
        
        var startTaskResult = await TaskService.EndTaskAsync(Guid.NewGuid());

        startTaskResult.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public async Task UpdateTaskDescriptionAsync_Success()
    {
        var taskName = Guid.NewGuid().ToString();
        var description = Guid.NewGuid().ToString();
        var projectName = Guid.NewGuid().ToString();
        var createProjectResult = await ProjectService.CreateProjectAsync(projectName);
        var createTaskResult = await TaskService.CreateTaskAsync(taskName, description, createProjectResult.Response.Id);

        var newDescription = Guid.NewGuid().ToString();
        await TaskService.UpdateTaskDescriptionAsync(createTaskResult.Response.Id, newDescription);

        var task = await DatabaseContextFixture.TaskEntities.FirstOrDefaultAsync(x => x.Id == createTaskResult.Response.Id);
        task.Description.Should().Be(newDescription);
    }
}