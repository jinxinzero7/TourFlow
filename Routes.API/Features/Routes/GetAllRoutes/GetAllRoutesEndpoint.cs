using FastEndpoints;
using Routes.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Routes.API.Features.Routes.GetAllRoutes;

public class GetAllRoutesEndpoint : EndpointWithoutRequest<List<GetAllRoutesResponse>>
{
    private readonly ApplicationDbContext _context;

    public GetAllRoutesEndpoint(ApplicationDbContext context)
    {
        _context = context;
    }

    // метод конфигурации эндпоинта
    public override void Configure()
    {
        Get("/api/routes");
        AllowAnonymous();
        Description(d => d
            .WithTags("Routes")
            .WithSummary("Get all routes"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var routes = await _context.Routes
            .Select(r => new GetAllRoutesResponse
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                BasePrice = r.BasePrice,
                DurationDays = r.DurationDays,
                IsActive = r.IsActive
            })
            .ToListAsync(ct);

        await Send.OkAsync(routes);
    }
}

// response dto
public class GetAllRoutesResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public int DurationDays { get; set; }
    public bool IsActive { get; set; }
}