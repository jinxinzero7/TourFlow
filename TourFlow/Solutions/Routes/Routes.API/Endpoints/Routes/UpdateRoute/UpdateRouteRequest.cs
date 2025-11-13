using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routes.API.Endpoints.Routes.CreateRoute;

namespace Routes.API.Endpoints.Routes.UpdateRoute
{
    public record UpdateRouteRequest
    {
        public Guid RouteId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal BasePrice { get; init; }
        public int DurationDays { get; init; }
        public List<RouteLocationRequest> Locations { get; init; } = new();
    }
}