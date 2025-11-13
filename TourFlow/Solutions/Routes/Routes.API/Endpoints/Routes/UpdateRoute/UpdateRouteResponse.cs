using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.API.Endpoints.Routes.UpdateRoute
{
    public record UpdateRouteResponse
    {
        public Guid RouteId { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}