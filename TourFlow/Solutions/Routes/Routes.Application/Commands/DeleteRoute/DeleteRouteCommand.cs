using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using Routes.Application.Common.Result;

namespace Routes.Application.Commands.DeleteRoute
{
    public record DeleteRouteCommand : ICommand<Result<DeleteRouteResult>>
    {
        public Guid RouteId { get; init; }
    }
}