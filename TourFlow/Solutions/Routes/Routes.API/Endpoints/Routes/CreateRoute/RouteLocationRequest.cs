using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.API.Endpoints.Routes.CreateRoute
{
    public record RouteLocationRequest
    {
        public string Location { get; init; } = string.Empty;
        public int StayDurationDays { get; init; }
    }
}