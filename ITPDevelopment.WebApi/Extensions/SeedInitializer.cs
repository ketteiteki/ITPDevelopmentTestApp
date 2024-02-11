using ITPDevelopment.Domain.Entities;
using ITPDevelopment.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ITPDevelopment.WebApi.Extensions;

public static class SeedInitializer
{
    public static async Task InitializeSeedsAsync(this IApplicationBuilder applicationBuilder)
    {
        using var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        await using var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();

        var areThereAnySeeds = await context.ProjectEntities.AnyAsync();

        if (areThereAnySeeds) return;

        var eventTriangleProject = new ProjectEntity("Event Triangle");
        var messengerProject = new ProjectEntity("Messenger");
        var mailApiProject = new ProjectEntity("MailApi");
        var secureAzureOidcProject = new ProjectEntity("SecureAzureOIDC");
        var yarpSignalRSampleProject = new ProjectEntity("YarpSignalRSample");

        var fishTextDescription = "Pellentesque et quam, amet, urna tempus risus mattis mauris amet imperdiet venenatis non imperdiet non nec habitasse sed sapien mauris dictum. Arcu et. Interdum luctus eleifend molestie amet, non odio. Sit tempus leo, vel efficitur nisi est. Lorem faucibus. Sit orci, sit vitae imperdiet sodales molestie sed libero, nunc sodales et. Pulvinar vulputate dui amet, dictum. Luctus dapibus arcu quam.";
        
        var taskList = new List<TaskEntity>
        {
            new TaskEntity("Implement oidc authentication", fishTextDescription, eventTriangleProject.Id),
            new TaskEntity("Fix TicketStore bug", fishTextDescription, eventTriangleProject.Id),
            new TaskEntity("Create seeds", fishTextDescription, eventTriangleProject.Id),
            new TaskEntity("Make adaptive frontend", fishTextDescription, eventTriangleProject.Id),
            new TaskEntity("Write tests for application", fishTextDescription, eventTriangleProject.Id),
            
            new TaskEntity("Implement realtime communication", fishTextDescription, messengerProject.Id),
            new TaskEntity("Add reactions for messages", fishTextDescription, messengerProject.Id),
            new TaskEntity("Add creating groups", fishTextDescription, messengerProject.Id),
            
            new TaskEntity("Add keycloak", fishTextDescription, mailApiProject.Id),
            new TaskEntity("Add dockerfile and docker compose yaml", fishTextDescription, mailApiProject.Id),
            new TaskEntity("Write test for database repositories", fishTextDescription, mailApiProject.Id),
            
            new TaskEntity("Translate from English to Russian", fishTextDescription, secureAzureOidcProject.Id),
            new TaskEntity("Write build latex pipeline", fishTextDescription, secureAzureOidcProject.Id),
            new TaskEntity("Find and correct errors in translation", fishTextDescription, secureAzureOidcProject.Id),
            
            new TaskEntity("Fix yarp config json", fishTextDescription, yarpSignalRSampleProject.Id),
            new TaskEntity("Add connection to signalR with jwt token", fishTextDescription, yarpSignalRSampleProject.Id),
            new TaskEntity("Add postman screenshots", fishTextDescription, yarpSignalRSampleProject.Id),
            new TaskEntity("Update README.md", fishTextDescription, yarpSignalRSampleProject.Id),
        };
        
        context.ProjectEntities.AddRange(eventTriangleProject, messengerProject, mailApiProject, secureAzureOidcProject, yarpSignalRSampleProject);
        context.TaskEntities.AddRange(taskList);
        await context.SaveChangesAsync();
    }
}