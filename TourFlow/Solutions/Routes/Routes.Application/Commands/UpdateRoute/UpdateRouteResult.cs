using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.Application.Commands.UpdateRoute
{
    public record UpdateRouteResult
    {
        public Guid RouteId { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}