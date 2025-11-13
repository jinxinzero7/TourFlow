using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.Application.Queries.GetRouteById
{
    public record GetRouteByIdResult
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal BasePrice { get; init; }
        public int DurationDays { get; init; }
        public bool IsActive { get; init; }
        public DateTime CreatedAt { get; init; }
        public List<RouteLocationResult> Locations { get; init; } = new();
    }

    public record RouteLocationResult
    {
        public string Location { get; init; } = string.Empty;
        public int StayDurationDays { get; init; }
    }
}