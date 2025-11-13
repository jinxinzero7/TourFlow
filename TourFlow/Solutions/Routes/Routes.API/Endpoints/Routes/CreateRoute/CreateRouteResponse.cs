using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.API.Endpoints.Routes.CreateRoute
{
    public record CreateRouteResponse
    {
        public Guid RouteId { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}