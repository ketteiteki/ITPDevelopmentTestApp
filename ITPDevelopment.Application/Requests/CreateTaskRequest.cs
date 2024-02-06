namespace ITPDevelopment.Application.Requests;

public record CreateTaskRequest(
    string TaskName,
    string Description,
    Guid ProjectId);