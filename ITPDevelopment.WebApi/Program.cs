using ITPDevelopment.Application.BusinessLogic;
using ITPDevelopment.Domain.Constants;
using ITPDevelopment.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration[AppSettingsConstants.DatabaseConnectionString];

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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();