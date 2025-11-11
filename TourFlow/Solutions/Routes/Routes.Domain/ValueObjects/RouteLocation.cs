using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.Domain.ValueObjects
{
    public record RouteLocation
    {
        public string Location { get; init; } = string.Empty;
        public int StayDurationDays { get; init; }

        // приватный конструктор
        private RouteLocation() { }

        // фабричный метод для создания локации
        public static RouteLocation Create(string location, int stayDurationDays)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentException("Location cannot be empty");
            if (stayDurationDays <= 0)
                throw new ArgumentException("Stay duration must be positive");

            return new RouteLocation
            {
                Location = location.Trim(),
                StayDurationDays = stayDurationDays
            };
        }
    }
}