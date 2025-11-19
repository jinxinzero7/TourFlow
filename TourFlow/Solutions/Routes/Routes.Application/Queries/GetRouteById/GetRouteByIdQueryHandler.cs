using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routes.Application.Common;
using FastEndpoints;
using Routes.Application.Common.Result;
using Routes.Domain.Interfaces.Repositories;

namespace Routes.Application.Queries.GetRouteById
{
    public class GetRouteByIdQueryHandler : ICommandHandler<GetRouteByIdQuery, Result<GetRouteByIdResult>>
    {
        private readonly IReadRouteRepository _readRepository;

        public GetRouteByIdQueryHandler(IReadRouteRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<Result<GetRouteByIdResult>> ExecuteAsync(
            GetRouteByIdQuery query,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var route = await _readRepository.GetByIdAsync(query.RouteId, cancellationToken);
                if (route is null)
                    return Result.Failure<GetRouteByIdResult>("Route not found", 404);

                var result = new GetRouteByIdResult
                {
                    Id = route.RouteId,
                    Name = route.Name,
                    Description = route.Description,
                    BasePrice = route.BasePrice,
                    DurationDays = route.DurationDays,
                    IsActive = route.IsActive,
                    CreatedAt = route.CreatedAt,
                    Locations = route.Locations.Select(l => new RouteLocationResult
                    {
                        Location = l.Location,
                        StayDurationDays = l.StayDurationDays
                    }).ToList()
                };

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<GetRouteByIdResult>($"Internal server error: {ex.Message}", 500);
            }
        }
    }
}