using ITPDevelopment.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ITPDevelopment.WebApi.Extensions;

public static class DatabaseMigrator
{
    public static async Task MigrateDatabaseAsync(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        await using var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();

        await context.Database.MigrateAsync();
    }
}