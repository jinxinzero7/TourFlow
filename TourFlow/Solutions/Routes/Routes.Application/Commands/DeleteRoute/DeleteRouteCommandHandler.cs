using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using Routes.Application.Common.Result;
using Routes.Domain.Interfaces.Repositories;

namespace Routes.Application.Commands.DeleteRoute
{
    public class DeleteRouteCommandHandler : ICommandHandler<DeleteRouteCommand, Result<DeleteRouteResult>>
    {
        private readonly IRouteRepository _routeRepository;

        public DeleteRouteCommandHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<Result<DeleteRouteResult>> ExecuteAsync(
            DeleteRouteCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var route = await _routeRepository.GetByIdAsync(command.RouteId, cancellationToken);
                if (route is null)
                    return Result.Failure<DeleteRouteResult>("Route not found", 404);

                // удаление
                await _routeRepository.DeleteAsync(route, cancellationToken);
                await _routeRepository.SaveChangesAsync(cancellationToken);

                return Result.Success(new DeleteRouteResult
                {
                    RouteId = route.RouteId,
                    Message = "Route deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return Result.Failure<DeleteRouteResult>($"Internal server error: {ex.Message}", 500);
            }
        }
    }
}