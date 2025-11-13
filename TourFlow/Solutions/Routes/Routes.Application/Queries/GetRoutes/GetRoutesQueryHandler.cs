using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using Routes.Application.Common.Result;
using Routes.Domain.Interfaces.Repositories;

namespace Routes.Application.Queries.GetRoutes
{
    public class GetRoutesQueryHandler : ICommandHandler<GetRoutesQuery, Result<GetRoutesResult>>
    {
        private readonly IReadRouteRepository _readRepository;

        public GetRoutesQueryHandler(IReadRouteRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<Result<GetRoutesResult>> ExecuteAsync(
            GetRoutesQuery query,
            CancellationToken cancellationToken = default)
        {
            try
            {
                List<Domain.Entities.Route> routes;

                // если в запросе указаны только активные маршруты - выдаем только те, у которых IsActive == true
                if (query.OnlyActive == true)
                {
                    routes = await _readRepository.GetActiveRoutesAsync(cancellationToken);
                }
                // иначе выдаем все
                else
                {
                    routes = await _readRepository.GetAllAsync(cancellationToken);
                }

                var result = new GetRoutesResult
                {
                    Routes = routes.Select(route => new RouteItemResult
                    {
                        Id = route.Id,
                        Name = route.Name,
                        Description = route.Description,
                        BasePrice = route.BasePrice,
                        DurationDays = route.DurationDays,
                        IsActive = route.IsActive,
                        CreatedAt = route.CreatedAt
                    }).ToList(),
                    TotalCount = routes.Count
                };

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<GetRoutesResult>($"Internal server error: {ex.Message}", 500);
            }
        }
    }
}