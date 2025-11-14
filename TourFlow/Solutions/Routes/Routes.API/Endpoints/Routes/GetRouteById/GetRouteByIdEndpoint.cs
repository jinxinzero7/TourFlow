using FastEndpoints;
using FastEndpoints.Swagger;
using Routes.Application.Common.Result;
using Routes.Application.Queries.GetRouteById;

namespace Routes.API.Endpoints.Routes.GetRouteById;

public class GetRouteByIdEndpoint : Endpoint<GetRouteByIdRequest, GetRouteByIdResponse>
{
    public override void Configure()
    {
        Get("/api/routes/{RouteId}");
        AllowAnonymous();
        Description(d => d
            .WithName("GetRouteById")
            .Produces<GetRouteByIdResponse>(200)
            .ProducesProblem(404)
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(GetRouteByIdRequest req, CancellationToken ct)
    {
        var query = new GetRouteByIdQuery
        {
            RouteId = req.RouteId,
        };
        var result = await query.ExecuteAsync(ct);

        if (result.IsSuccess)
        {
            var response = new GetRouteByIdResponse
            {
                Id = result.Value.Id,
                Name = result.Value.Name,
                Description = result.Value.Description,
                BasePrice = result.Value.BasePrice,
                DurationDays = result.Value.DurationDays,
                IsActive = result.Value.IsActive,
                CreatedAt = result.Value.CreatedAt,
                Locations = result.Value.Locations.Select(l => new RouteLocationResponse
                {
                    Location = l.Location,
                    StayDurationDays = l.StayDurationDays
                }).ToList()
            };

            await Send.OkAsync(response, ct);
        }
        else
        {
            AddError(result.Error);
            await Send.ErrorsAsync(result.StatusCode, ct);        
        }
    }
}