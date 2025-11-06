using FastEndpoints;
using FastEndpoints.Swagger;
using Routes.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// конфигурация json файла
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

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

// database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// тестовая инициализация базы данных
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    // Создаем базу если не существует
    await context.Database.EnsureCreatedAsync();
    
    // Добавляем тестовые данные если таблица пуста
    if (!context.Routes.Any())
    {
        var route = Routes.API.Domain.Entities.Route.Create(
            "Тур по Золотому кольцу России", 
            "Классический тур по древним городам России", 
            25000m, 
            7);
            
        route.AddLocation("Москва", 2);
        route.AddLocation("Сергиев Посад", 1);
        route.AddLocation("Переславль-Залесский", 1);
        route.AddLocation("Ярославль", 2);
        route.AddLocation("Кострома", 1);
        
        context.Routes.Add(route);
        await context.SaveChangesAsync();
    }
}

// Middleware pipeline
app.UseFastEndpoints();
app.UseSwaggerGen();

app.Run();