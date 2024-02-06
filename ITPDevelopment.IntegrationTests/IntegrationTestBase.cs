using ITPDevelopment.Application.BusinessLogic;
using ITPDevelopment.Domain.Constants;
using ITPDevelopment.IntegrationTests.Configuration;
using ITPDevelopment.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ITPDevelopment.IntegrationTests;

[Collection("Sequence")]
public class IntegrationTestBase : IAsyncLifetime
{
    protected readonly DatabaseContext DatabaseContextFixture;
    protected readonly TaskService TaskService;
    protected readonly ProjectService ProjectService;

    public IntegrationTestBase()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var databaseConnectionString = configuration[AppSettingsConstants.DatabaseConnectionStringForIntegrationTests];
        
        ArgumentException.ThrowIfNullOrEmpty(databaseConnectionString);
        
        var serviceProvider = ITPDevelopmentStartup.Initialize(databaseConnectionString);

        TaskService = serviceProvider.GetRequiredService<TaskService>();
        ProjectService = serviceProvider.GetRequiredService<ProjectService>();

        DatabaseContextFixture = serviceProvider.GetRequiredService<DatabaseContext>();
    }
    
    public async Task InitializeAsync()
    {
        await DatabaseContextFixture.Database.MigrateAsync();

        var query = """
                    truncate table "ProjectEntities" cascade;
                    truncate table "TaskEntities" cascade;
                    """;

        await DatabaseContextFixture.Database.ExecuteSqlRawAsync(query);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}