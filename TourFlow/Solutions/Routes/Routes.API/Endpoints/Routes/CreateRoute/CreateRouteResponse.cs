using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.API.Endpoints.Routes.CreateRoute
{
    public class CreateRouteResponse
    {
        public Guid RouteId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}