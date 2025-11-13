using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.API.Endpoints.Routes.GetRoutes
{
    public record GetRoutesRequest
    {
        public bool? OnlyActive { get; init; }
    }
}