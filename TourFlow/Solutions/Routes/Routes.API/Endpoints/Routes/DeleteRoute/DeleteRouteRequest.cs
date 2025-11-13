using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.API.Endpoints.Routes.DeleteRoute
{
    public record DeleteRouteRequest
    {
        public Guid RouteId { get; init; }
    }
}