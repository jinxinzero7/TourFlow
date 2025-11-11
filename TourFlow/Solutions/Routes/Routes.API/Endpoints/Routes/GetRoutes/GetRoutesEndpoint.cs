using FastEndpoints;
using FastEndpoints.Swagger;
using Routes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Routes.API.Endpoints.Routes.GetRoutes;

public class GetRoutesEndpoint : EndpointWithoutRequest<List<GetRoutesResponse>>
{
    private readonly ApplicationDbContext _context;

    public GetRoutesEndpoint(ApplicationDbContext context)
    {
        _context = context;
    }

    // метод конфигурации эндпоинта
    public override void Configure()
    {
        Get("/api/routes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var routes = await _context.Routes
            .Select(r => new GetRoutesResponse
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