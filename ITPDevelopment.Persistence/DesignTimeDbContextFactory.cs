using ITPDevelopment.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ITPDevelopment.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var databaseConnectionString =
            configuration[AppSettingsConstants.DatabaseConnectionString] ??
            throw new ApplicationException(AppSettingsConstants.DatabaseConnectionString);

        var options = new DbContextOptionsBuilder<DatabaseContext>();

        options.UseNpgsql(databaseConnectionString);

        return new DatabaseContext(options.Options);
    }
}
