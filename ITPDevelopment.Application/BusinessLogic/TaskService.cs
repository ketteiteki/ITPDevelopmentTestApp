using ITPDevelopment.Domain.Constants;
using ITPDevelopment.Domain.Entities;
using ITPDevelopment.Domain.Responses;
using ITPDevelopment.Domain.Responses.Errors;
using ITPDevelopment.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ITPDevelopment.Application.BusinessLogic;

public class TaskService(DatabaseContext context)
{
    public async Task<Result<List<TaskEntity>>> GetTaskListAsync(int offset, int limit)
    {
        var taskList = await context.TaskEntities
            .Include(x => x.Project)
            .OrderBy(x => x.CreateDate)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return new Result<List<TaskEntity>>(taskList);
    }
    
    public async Task<Result<TaskEntity>> CreateTaskAsync(string taskName, string description, Guid projectId)
    {
        var task = new TaskEntity(taskName, description, projectId);

        context.TaskEntities.Add(task);
        await context.SaveChangesAsync();

        await context.Entry(task).Reference(x => x.Project).LoadAsync();
        
        return new Result<TaskEntity>(task);
    }
    
    public async Task<Result<TaskEntity>> StartTaskAsync(Guid taskId)
    {
        var task = await context.TaskEntities.FirstOrDefaultAsync(x => x.Id == taskId);

        if (task == null)
        {
            return new Result<TaskEntity>(new Error(ErrorResponseConstants.TaskNotFound));
        }
        
        if (task.StartDate != null)
        {
            return new Result<TaskEntity>(new Error(ErrorResponseConstants.DateHasAlreadyBeenSet));
        }
        
        task.StartDate = DateTime.UtcNow;
        task.UpdateDate = DateTime.UtcNow;
        
        context.TaskEntities.Update(task);
        await context.SaveChangesAsync();

        return new Result<TaskEntity>(task);
    }
    
    public async Task<Result<TaskEntity>> EndTaskAsync(Guid taskId)
    {
        var task = await context.TaskEntities.FirstOrDefaultAsync(x => x.Id == taskId);

        if (task == null)
        {
            return new Result<TaskEntity>(new Error(ErrorResponseConstants.TaskNotFound));
        }

        if (task.EndDate != null)
        {
            return new Result<TaskEntity>(new Error(ErrorResponseConstants.DateHasAlreadyBeenSet));
        }
        
        task.EndDate = DateTime.UtcNow;
        task.UpdateDate = DateTime.UtcNow;
        
        context.TaskEntities.Update(task);
        await context.SaveChangesAsync();

        return new Result<TaskEntity>(task);
    }
    
    public async Task<Result<TaskEntity>> UpdateTaskDescriptionAsync(Guid taskId, string description)
    {
        var task = await context.TaskEntities.FirstOrDefaultAsync(x => x.Id == taskId);

        if (task == null)
        {
            return new Result<TaskEntity>(new Error(ErrorResponseConstants.TaskNotFound));
        }
        
        task.Description = description;
        task.UpdateDate = DateTime.UtcNow;
        
        context.TaskEntities.Update(task);
        await context.SaveChangesAsync();

        return new Result<TaskEntity>(task);
    }
}