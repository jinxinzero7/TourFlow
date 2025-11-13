using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using Routes.Application.Common.Result;

namespace Routes.Application.Queries.GetRouteById
{
    public record GetRouteByIdQuery : ICommand<Result<GetRouteByIdResult>>
    {
        public Guid RouteId { get; init; }
    }
}