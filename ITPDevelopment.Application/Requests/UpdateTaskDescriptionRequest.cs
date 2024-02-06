namespace ITPDevelopment.Application.Requests;

public record UpdateTaskDescriptionRequest(
    Guid TaskId,
    string Description);
