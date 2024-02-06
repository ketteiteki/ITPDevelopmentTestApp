using ITPDevelopment.Application.BusinessLogic;
using ITPDevelopment.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITPDevelopment.IntegrationTests.Configuration;

public static class ITPDevelopmentStartup
{
    public static IServiceProvider Initialize(string connectionString)
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddDbContext<DatabaseContext>(option =>
        {
            option.UseNpgsql(connectionString);
        });
        
        serviceCollection.AddSingleton<TaskService>();
        serviceCollection.AddSingleton<ProjectService>();
        
        return serviceCollection.BuildServiceProvider();
    }
}