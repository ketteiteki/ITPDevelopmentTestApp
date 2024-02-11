#nullable disable

using FluentAssertions;
using Xunit;

namespace ITPDevelopment.IntegrationTests.BusinessLogicTests;

public class ProjectServiceTests : IntegrationTestBase
{
    [Fact]
    public async Task GetProjectListAsync_Success()
    {
        await ProjectService.CreateProjectAsync(Guid.NewGuid().ToString());
        await ProjectService.CreateProjectAsync(Guid.NewGuid().ToString());
        await ProjectService.CreateProjectAsync(Guid.NewGuid().ToString());

        var result = await ProjectService.GetProjectListAsync();

        result.Response.Count.Should().Be(3);
    }
    
    [Fact]
    public async Task CreateProjectAsync_Success()
    {
        var projectName = Guid.NewGuid().ToString();
        
        var createProjectResult = await ProjectService.CreateProjectAsync(projectName);

        createProjectResult.IsSuccess.Should().BeTrue();
        createProjectResult.Response.ProjectName.Should().Be(projectName);
    }
}