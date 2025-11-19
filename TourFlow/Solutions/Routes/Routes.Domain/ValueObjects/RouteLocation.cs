using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routes.Domain.Common;
using Routes.Domain.Entities;

namespace Routes.Domain.ValueObjects
{
    public class RouteLocation
    {
        public string Location { get; private set; } = string.Empty;
        public int StayDurationDays { get; private set; }

        // приватный конструктор для EF
        private RouteLocation() { } 

        public static RouteLocation Create(string location, int stayDurationDays)
        {
            return new RouteLocation
            {
                Location = location,
                StayDurationDays = stayDurationDays
            };
        }
    }
}