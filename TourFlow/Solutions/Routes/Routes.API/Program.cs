using FastEndpoints;
using FastEndpoints.Swagger;
using Routes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// FastEndpoints
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.DocumentName = "Routes API";
        s.Title = "TourFlow Routes Service";
        s.Version = "v1.0";
    };
});

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Middleware
app.UseFastEndpoints();
app.UseSwaggerGen(); // ← Используй это вместо UseSwaggerGen

app.Run();