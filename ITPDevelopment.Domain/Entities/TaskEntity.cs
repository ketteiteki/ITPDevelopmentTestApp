namespace ITPDevelopment.Domain.Entities;

public class TaskEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string TaskName { get; set; }
    
    public string Description { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public ProjectEntity Project { get; set; }
    
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public DateTime UpdateDate { get; set; }

    public TaskEntity(string taskName, string description, Guid projectId)
    {
        TaskName = taskName;
        Description = description;
        ProjectId = projectId;
        CreateDate = DateTime.UtcNow;
        UpdateDate = DateTime.UtcNow;
    }
}