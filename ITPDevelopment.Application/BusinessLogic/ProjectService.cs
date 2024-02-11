using ITPDevelopment.Domain.Entities;
using ITPDevelopment.Domain.Responses;
using ITPDevelopment.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ITPDevelopment.Application.BusinessLogic;

public class ProjectService(DatabaseContext context)
{
    public async Task<Result<List<ProjectEntity>>> GetProjectListAsync()
    {
        var projectList = await context.ProjectEntities
            .OrderBy(x => x.CreateDate)
            .ToListAsync();

        return new Result<List<ProjectEntity>>(projectList);
    }
    
    public async Task<Result<ProjectEntity>> CreateProjectAsync(string projectName)
    {
        var project = new ProjectEntity(projectName);

        context.ProjectEntities.Add(project);
        await context.SaveChangesAsync();

        return new Result<ProjectEntity>(project);
    }
}