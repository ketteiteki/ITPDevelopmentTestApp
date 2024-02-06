namespace ITPDevelopment.Domain.Entities;

public class ProjectEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string ProjectName { get; set; }
    
    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public ProjectEntity(string projectName)
    {
        ProjectName = projectName;
        CreateDate = DateTime.UtcNow;
        UpdateDate = DateTime.UtcNow;
    }
}