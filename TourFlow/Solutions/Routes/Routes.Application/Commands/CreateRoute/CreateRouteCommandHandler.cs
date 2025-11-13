using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routes.Application.Common;
using Routes.Domain.Interfaces.Repositories;
using FastEndpoints;
using Routes.Application.Common.Result;

namespace Routes.Application.Commands.CreateRoute
{
    public class CreateRouteCommandHandler : ICommandHandler<CreateRouteCommand, Result<CreateRouteResult>>
    {
        private readonly IRouteRepository _routeRepository;

        public CreateRouteCommandHandler(
            IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public async Task<Result<CreateRouteResult>> ExecuteAsync(
        CreateRouteCommand command,
        CancellationToken cancellationToken = default)
        {
            try
            {
                // создание
                var route = Domain.Entities.Route.Create(
                    command.Name,
                    command.Description,
                    command.BasePrice,
                    command.DurationDays);

                // добавление локаций
                foreach (var location in command.Locations)
                {
                    route.AddLocation(location.Location, location.StayDurationDays);
                }

                await _routeRepository.AddAsync(route, cancellationToken);
                await _routeRepository.SaveChangesAsync(cancellationToken);

                return Result.Success(new CreateRouteResult
                {
                    RouteId = route.Id,
                    Message = "Route created successfully"
                });
            }
            catch (Exception ex)
            {
                return Result.Failure<CreateRouteResult>($"Internal server error: {ex.Message}", 500);
            }
        }
    }
}