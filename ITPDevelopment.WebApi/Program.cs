using ITPDevelopment.Application.BusinessLogic;
using ITPDevelopment.Domain.Constants;
using ITPDevelopment.Persistence;
using ITPDevelopment.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration[AppSettingsConstants.DatabaseConnectionString];
var allowedOrigins = builder.Configuration.GetSection(AppSettingsConstants.AllowedOrigins).Get<string[]>();

ArgumentException.ThrowIfNullOrEmpty(connectionString);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<TaskService>();

builder.Services.AddDbContext<DatabaseContext>(option =>
{
    option.UseNpgsql(connectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy  =>
        {
            policy
                .WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.MapControllers();

await app.InitializeSeedsAsync();

app.Run();