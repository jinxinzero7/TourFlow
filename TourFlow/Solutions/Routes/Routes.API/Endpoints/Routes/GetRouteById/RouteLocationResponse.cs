using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.API.Endpoints.Routes.GetRouteById
{
    public record RouteLocationResponse
    {
        public string Location { get; init; } = string.Empty;
        public int StayDurationDays { get; init; }
    }
}