using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routes.Domain.Common;
using Routes.Domain.Entities;

namespace Routes.Domain.ValueObjects
{
    public class RouteLocation : BaseEntity
    {
        public string Location { get; private set; } = string.Empty;
        public int StayDurationDays { get; private set; }

        // навигационные свойства для связи с Route
        public Guid RouteId { get; private set; }
        public Route Route { get; private set; } = null!;

        // приватный конструктор для EF
        private RouteLocation() { } 

        public static RouteLocation Create(string location, int stayDurationDays)
        {
            return new RouteLocation
            {
                Id = Guid.NewGuid(),
                Location = location,
                StayDurationDays = stayDurationDays
            };
        }
    }
}