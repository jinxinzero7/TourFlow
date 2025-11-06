using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routes.API.Domain.ValueObjects;

namespace Routes.API.Domain.Entities
{
    public class Route
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal BasePrice { get; private set; }
        public int DurationDays { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        // value objects как owned types
        private readonly List<RouteLocation> _locations = new();
        public IReadOnlyCollection<RouteLocation> Locations => _locations.AsReadOnly();

        // приватный конструктор
        private Route() { }

        // фабричный метод создания route
        public static Route Create(string name, string description, decimal basePrice, int durationDays)
        {
            // валидация и возвращение нового route
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Route name cannot be empty");

            return new Route
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                Description = description?.Trim() ?? string.Empty,
                BasePrice = basePrice > 0 ? basePrice : throw new ArgumentException("Base price must be positive"),
                DurationDays = durationDays > 0 ? durationDays : throw new ArgumentException("Duration must be positive"),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
        }

        public void AddLocation(string location, int stayDurationDays)
        {
            _locations.Add(RouteLocation.Create(location, stayDurationDays));
        }
    }
}