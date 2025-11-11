using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Routes.Domain.Entities;
using Routes.Infrastructure.Data;
using Routes.API.Endpoints.Routes.GetRouteById;

namespace Routes.API.Endpoints.Routes.CreateRoute;

public class CreateRouteEndpoint : Endpoint<CreateRouteRequest, CreateRouteResponse>
{
    private readonly ApplicationDbContext _context;

    public CreateRouteEndpoint(ApplicationDbContext context)
    {
        _context = context;
    }

    // метод конфигурации эндпоинта
    public override void Configure()
    {
        Post("/api/routes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateRouteRequest req, CancellationToken ct)
    {
        try
        {
            // создание маршрута
            var route = Domain.Entities.Route.Create(req.Name, req.Description, req.BasePrice, req.DurationDays);

            // добавление локации
            foreach (var location in req.Locations)
            {
                route.AddLocation(location.Location, location.StayDurationDays);
            }

            _context.Routes.Add(route);
            await _context.SaveChangesAsync(ct);

            await Send.CreatedAtAsync<GetRouteByIdEndpoint>(
                new { routeId = route.Id },
                new CreateRouteResponse
                {
                    RouteId = route.Id,
                    Message = "Route created successfully"
                });
        }
        catch (ArgumentException ex)
        {
            AddError(ex.Message);
            await Send.ErrorsAsync(400, ct);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error creating route");
            AddError("Internal server error");
            await Send.ErrorsAsync(500, ct);
        }
    }
}
