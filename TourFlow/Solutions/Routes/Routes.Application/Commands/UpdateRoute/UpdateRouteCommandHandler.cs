using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using Routes.Application.Common.Result;
using Routes.Domain.Interfaces.Repositories;

namespace Routes.Application.Commands.UpdateRoute
{
    public class UpdateRouteCommandHandler : ICommandHandler<UpdateRouteCommand, Result<UpdateRouteResult>>
    {
        private readonly IRouteRepository _routeRepository;

        public UpdateRouteCommandHandler(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<Result<UpdateRouteResult>> ExecuteAsync(
            UpdateRouteCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var route = await _routeRepository.GetByIdAsync(command.RouteId, cancellationToken);
                if (route is null)
                    return Result.Failure<UpdateRouteResult>("Route not found", 404);

                // обновление данных
                route.UpdateDetails(command.Name, command.Description, command.BasePrice, command.DurationDays);

                // удаляем старые локации
                route.ClearLocations();
                
                // добавляем новые локации
                foreach (var location in command.Locations)
                {
                    route.AddLocation(location.Location, location.StayDurationDays);
                }

                await _routeRepository.SaveChangesAsync(cancellationToken);

                return Result.Success(new UpdateRouteResult
                {
                    RouteId = route.RouteId,
                    Message = "Route updated successfully"
                });
            }
            catch (Exception ex)
            {
                return Result.Failure<UpdateRouteResult>($"Internal server error: {ex.Message}", 500);
            }
        }
    }
}