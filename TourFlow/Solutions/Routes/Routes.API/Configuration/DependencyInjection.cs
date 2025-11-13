using FastEndpoints;
using FastEndpoints.Swagger;
using Routes.Application;
using Routes.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Routes.API.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection UseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFastEndpoints(options =>
        {
            options.Assemblies =
            [
                typeof(DependencyInjection).Assembly,
                typeof(Application.ServiceExtensions).Assembly
            ];
            options.IncludeAbstractValidators = true;
        })
        .SwaggerDocument(o =>
        {
            o.DocumentSettings = s =>
            {
                s.DocumentName = "v1";
                s.Title = "TourFlow Routes API";
                s.Version = "v1";
                s.Description = """
                    ## 🗺️ TourFlow Routes API
                    API для управления маршрутами туристического агентства
                    """;
            };
            o.ShortSchemaNames = true;
        });

        services.AddInfrastructure(configuration);
        services.AddApplication();

        return services;
    }

    public static IApplicationBuilder UseConfiguration(this WebApplication app)
    {
        app.UseFastEndpoints(config =>
        {
            config.Endpoints.RoutePrefix = "api";
            config.Endpoints.ShortNames = true;
        });

        app.UseSwaggerGen();

        return app;
    }
}