using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.API.Endpoints.Routes.GetRoutes
{
    public record GetRoutesResponse
    {
        public List<RouteItemResponse> Routes { get; init; } = new();
        public int TotalCount { get; init; }
    }

    public record RouteItemResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal BasePrice { get; init; }
        public int DurationDays { get; init; }
        public bool IsActive { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}