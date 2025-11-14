using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using Routes.Application.Commands.DeleteRoute;
using Routes.Application.Common.Result;

namespace Routes.API.Endpoints.Routes.DeleteRoute
{
    public class DeleteRouteEndpoint : Endpoint<DeleteRouteRequest>
    {
        public override void Configure()
        {
            Delete("/api/routes/{RouteId}");
            AllowAnonymous();
            Description(d => d
                .WithName("DeleteRoute")
                .Produces(204)
                .ProducesProblem(404)
                .ProducesProblem(500));
        }

        public override async Task HandleAsync(DeleteRouteRequest req, CancellationToken ct)
        {
            var command = new DeleteRouteCommand { RouteId = req.RouteId };
            var result = await command.ExecuteAsync(ct);

            if (result.IsSuccess)
            {
                await Send.NoContentAsync(ct);
            }
            else
            {
                AddError(result.Error);
                await Send.ErrorsAsync(result.StatusCode, ct);            
            }
        }
    }
}