using FastEndpoints;
using FastEndpoints.Swagger;
using Routes.Application.Queries.GetRoutes;
using Routes.Application.Common.Result;

namespace Routes.API.Endpoints.Routes.GetRoutes
{
    public class GetRoutesEndpoint : Endpoint<GetRoutesRequest, GetRoutesResponse>
{
    private readonly ICommandHandler<GetRoutesQuery, Result<GetRoutesResult>> _queryHandler;

    public GetRoutesEndpoint(ICommandHandler<GetRoutesQuery, Result<GetRoutesResult>> queryHandler)
    {
        _queryHandler = queryHandler;
    }

    public override void Configure()
    {
        Get("/api/routes");
        AllowAnonymous();
        Description(d => d
            .WithName("GetRoutes")
            .Produces<GetRoutesResponse>(200)
            .ProducesProblem(500));
    }

    public override async Task HandleAsync(GetRoutesRequest req, CancellationToken ct)
    {
        var query = new GetRoutesQuery { OnlyActive = req.OnlyActive };
        var result = await _queryHandler.ExecuteAsync(query, ct);

        if (result.IsSuccess)
        {
            var response = new GetRoutesResponse
            {
                Routes = result.Value.Routes.Select(route => new RouteItemResponse
                {
                    Id = route.Id,
                    Name = route.Name,
                    Description = route.Description,
                    BasePrice = route.BasePrice,
                    DurationDays = route.DurationDays,
                    IsActive = route.IsActive,
                    CreatedAt = route.CreatedAt
                }).ToList(),
                TotalCount = result.Value.TotalCount
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
}
