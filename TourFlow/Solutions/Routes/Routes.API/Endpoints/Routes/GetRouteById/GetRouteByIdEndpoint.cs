using FastEndpoints;
using FastEndpoints.Swagger;
using Routes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Routes.API.Endpoints.Routes.GetRouteById;

public class GetRouteByIdEndpoint : Endpoint<GetRouteByIdRequest, GetRouteByIdResponse>
{
    private readonly ApplicationDbContext _context;

    public GetRouteByIdEndpoint(ApplicationDbContext context)
    {
        _context = context;
    }

    // метод конфигурации эндпоинта
    public override void Configure()
    {
        Get("/api/routes/{RouteId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetRouteByIdRequest req, CancellationToken ct)
    {
        var route = await _context.Routes
            .FirstOrDefaultAsync(r => r.Id == req.RouteId, ct);

        if (route is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var response = new GetRouteByIdResponse
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