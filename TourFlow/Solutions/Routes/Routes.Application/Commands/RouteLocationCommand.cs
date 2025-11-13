using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routes.Application.Commands
{
    public record RouteLocationCommand
    {
        public string Location { get; init; } = string.Empty;
        public int StayDurationDays { get; init; }
    }
}