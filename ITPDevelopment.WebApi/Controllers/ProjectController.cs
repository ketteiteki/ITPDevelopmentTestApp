using ITPDevelopment.Application.BusinessLogic;
using ITPDevelopment.Application.Requests;
using ITPDevelopment.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ITPDevelopment.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class ProjectController(ProjectService projectService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
    {
        var result = await projectService.CreateProjectAsync(request.ProjectName);

        return result.ToActionResult();
    }
}