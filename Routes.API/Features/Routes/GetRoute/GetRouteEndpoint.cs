using FastEndpoints;
using Routes.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Routes.API.Features.Routes.GetRoute;

public class GetRouteEndpoint : Endpoint<GetRouteRequest, GetRouteResponse>
{
    private readonly ApplicationDbContext _context;

    public GetRouteEndpoint(ApplicationDbContext context)
    {
        _context = context;
    }

    // метод конфигурации эндпоинта
    public override void Configure()
    {
        Get("/api/routes/{RouteId}");
        AllowAnonymous();
        Description(d => d
            .WithTags("Routes")
            .Produces<GetRouteResponse>(200)
            .ProducesProblem(404)
            .WithSummary("Get route by ID"));
    }

    public override async Task HandleAsync(GetRouteRequest req, CancellationToken ct)
    {
        var route = await _context.Routes
            .FirstOrDefaultAsync(r => r.Id == req.RouteId, ct);

        if (route is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var response = new GetRouteResponse
        {
            Id = route.Id,
            Name = route.Name,
            Description = route.Description,
            BasePrice = route.BasePrice,
            DurationDays = route.DurationDays,
            IsActive = route.IsActive
        };

        await Send.OkAsync(response);
    }
}

public class GetRouteRequest
{
    public Guid RouteId { get; set; }
}

public class GetRouteResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public int DurationDays { get; set; }
    public bool IsActive { get; set; }
}