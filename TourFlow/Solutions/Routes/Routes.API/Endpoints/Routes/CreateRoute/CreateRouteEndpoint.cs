using FastEndpoints;
using Routes.Application.Commands;
using Routes.Application.Commands.CreateRoute;
using Routes.Application.Common.Result;
using Routes.API.Endpoints.Routes.GetRouteById;

namespace Routes.API.Endpoints.Routes.CreateRoute
{
    public class CreateRouteEndpoint : Endpoint<CreateRouteRequest, CreateRouteResponse>
    {
        private readonly ICommandHandler<CreateRouteCommand, Result<CreateRouteResult>> _commandHandler;

        public CreateRouteEndpoint(ICommandHandler<CreateRouteCommand, Result<CreateRouteResult>> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public override void Configure()
        {
            Post("/api/routes");
            AllowAnonymous();
            Description(d => d
                .WithName("CreateRoute")
                .Produces<CreateRouteResponse>(201)
                .ProducesProblem(400)
                .ProducesProblem(500));
        }

        public override async Task HandleAsync(CreateRouteRequest req, CancellationToken ct)
        {
            var command = new CreateRouteCommand
            {
                Name = req.Name,
                Description = req.Description,
                BasePrice = req.BasePrice,
                DurationDays = req.DurationDays,
                Locations = req.Locations.Select(l => new RouteLocationCommand
                {
                    Location = l.Location,
                    StayDurationDays = l.StayDurationDays
                }).ToList()
            };

            var result = await _commandHandler.ExecuteAsync(command, ct);

            if (result.IsSuccess)
            {
                await Send.CreatedAtAsync<GetRouteByIdEndpoint>(
                    new { routeId = result.Value.RouteId },
                    new CreateRouteResponse
                    {
                        RouteId = result.Value.RouteId,
                        Message = result.Value.Message
                    },
                    cancellation: ct);
            }
            else
            {
                AddError(result.Error);
                await Send.ErrorsAsync(result.StatusCode, ct);
            }
        }
    }
}

