#nullable disable

using ITPDevelopment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITPDevelopment.Persistence;

public class DatabaseContext : DbContext
{
    public DbSet<ProjectEntity> ProjectEntities { get; set; }
    public DbSet<TaskEntity> TaskEntities { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions)
    {
    }
	
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<TaskEntity>()
            .HasOne(x => x.Project)
            .WithMany()
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}