using ITPDevelopment.Application.BusinessLogic;
using ITPDevelopment.Application.Requests;
using ITPDevelopment.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ITPDevelopment.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class TaskController(TaskService taskService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTaskList([FromQuery] int offset = 0, [FromQuery] int limit = 30)
    {
        var result = await taskService.GetTaskListAsync(offset, limit);

        return result.ToActionResult();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        var result = await taskService.CreateTaskAsync(request.TaskName, request.Description, request.ProjectId);

        return result.ToActionResult();
    }
    
    [HttpPost("Start")]
    public async Task<IActionResult> StartTask([FromQuery] Guid taskId)
    {
        var result = await taskService.StartTaskAsync(taskId);

        return result.ToActionResult();
    }
    
    [HttpPost("End")]
    public async Task<IActionResult> EndTask([FromQuery] Guid taskId)
    {
        var result = await taskService.EndTaskAsync(taskId);

        return result.ToActionResult();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateTaskDescription([FromBody] UpdateTaskDescriptionRequest request)
    {
        var result = await taskService.UpdateTaskDescriptionAsync(request.TaskId, request.Description);

        return result.ToActionResult();
    }
}