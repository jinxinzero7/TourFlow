using FastEndpoints;
using Routes.Application.Commands;
using Routes.Application.Commands.UpdateRoute;
using Routes.Application.Common.Result;

namespace Routes.API.Endpoints.Routes.UpdateRoute
{
    public class UpdateRouteEndpoint : Endpoint<UpdateRouteRequest, UpdateRouteResponse>
    {
        private readonly ICommandHandler<UpdateRouteCommand, Result<UpdateRouteResult>> _commandHandler;

        public UpdateRouteEndpoint(ICommandHandler<UpdateRouteCommand, Result<UpdateRouteResult>> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public override void Configure()
        {
            Put("/api/routes/{RouteId}");
            AllowAnonymous();
            Description(d => d
                .WithName("UpdateRoute")
                .Produces<UpdateRouteResponse>(200)
                .ProducesProblem(400)
                .ProducesProblem(404)
                .ProducesProblem(500));
        }

        public override async Task HandleAsync(UpdateRouteRequest req, CancellationToken ct)
        {
            var command = new UpdateRouteCommand
            {
                RouteId = req.RouteId,
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
                var response = new UpdateRouteResponse
                {
                    RouteId = result.Value.RouteId,
                    Message = result.Value.Message
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
