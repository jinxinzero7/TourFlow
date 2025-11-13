using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.Application.Queries.GetRoutes
{
    public record GetRoutesResult
    {
        public List<RouteItemResult> Routes { get; init; } = new();
        public int TotalCount { get; init; }
    }

    public record RouteItemResult
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