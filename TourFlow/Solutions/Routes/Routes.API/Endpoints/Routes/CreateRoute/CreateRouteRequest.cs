using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.API.Endpoints.Routes.CreateRoute
{
    public class CreateRouteRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public int DurationDays { get; set; }
        public List<RouteLocationRequest> Locations { get; set; } = new();
    }
}