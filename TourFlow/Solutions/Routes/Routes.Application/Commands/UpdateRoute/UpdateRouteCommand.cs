using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using Routes.Application.Common.Result;
using Routes.Application.Commands;

namespace Routes.Application.Commands.UpdateRoute
{
    public record UpdateRouteCommand : ICommand<Result<UpdateRouteResult>>
    {
        public Guid RouteId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal BasePrice { get; init; }
        public int DurationDays { get; init; }
        public List<RouteLocationCommand> Locations { get; init; } = new();
    }
}