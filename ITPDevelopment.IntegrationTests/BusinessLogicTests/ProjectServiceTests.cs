#nullable disable

using FluentAssertions;
using Xunit;

namespace ITPDevelopment.IntegrationTests.BusinessLogicTests;

public class ProjectServiceTests : IntegrationTestBase
{
    [Fact]
    public async Task CreateProjectAsync_Success()
    {
        var projectName = Guid.NewGuid().ToString();
        
        var createProjectResult = await ProjectService.CreateProjectAsync(projectName);

        createProjectResult.IsSuccess.Should().BeTrue();
        createProjectResult.Response.ProjectName.Should().Be(projectName);
    }
}