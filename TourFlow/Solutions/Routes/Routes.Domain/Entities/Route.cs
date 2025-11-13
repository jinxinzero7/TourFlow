using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routes.Domain.Common;
using Routes.Domain.ValueObjects;

namespace Routes.Domain.Entities
{
    public class Route : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal BasePrice { get; private set; }
        public int DurationDays { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public bool IsActive { get; private set; } = true;

        // value objects как owned types
        private readonly List<RouteLocation> _locations = new();
        public IReadOnlyCollection<RouteLocation> Locations => _locations.AsReadOnly();

        // приватный конструктор для EF
        private Route() { }

        // фабричный метод создания route
        public static Route Create(string name, string description, decimal basePrice, int durationDays)
        {
            return new Route
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                Description = description?.Trim() ?? string.Empty,
                BasePrice = basePrice,
                DurationDays = durationDays,
                CreatedAt = DateTime.UtcNow,
            };
        }

        // entity методы для создания и изменения сущности
        public void AddLocation(string location, int stayDurationDays)
        {
            _locations.Add(RouteLocation.Create(location, stayDurationDays));
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(string name, string description, decimal basePrice, int durationDays)
        {
            Name = name;
            Description = description;
            BasePrice = basePrice;
            DurationDays = durationDays;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ClearLocations()
        {
            _locations.Clear();
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}