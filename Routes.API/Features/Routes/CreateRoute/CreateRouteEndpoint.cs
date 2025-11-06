using FastEndpoints;
using Routes.API.Domain.Entities;
using Routes.API.Infrastructure.Data;
using Routes.API.Features.Routes.GetRoute;

namespace Routes.API.Features.Routes.CreateRoute;

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
        Description(d => d
            .WithTags("Routes")
            .Produces<CreateRouteResponse>(201)
            .ProducesProblem(400)
            .WithSummary("Create a new travel route"));
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

            await Send.CreatedAtAsync<GetRouteEndpoint>(
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

// request Dto
public class CreateRouteRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public int DurationDays { get; set; }
    public List<RouteLocationRequest> Locations { get; set; } = new();
}

// request DTO for location
public record RouteLocationRequest
{
    public string Location { get; init; } = string.Empty;
    public int StayDurationDays { get; init; }
}

// response DTO
public class CreateRouteResponse
{
    public Guid RouteId { get; set; }
    public string Message { get; set; } = string.Empty;
}