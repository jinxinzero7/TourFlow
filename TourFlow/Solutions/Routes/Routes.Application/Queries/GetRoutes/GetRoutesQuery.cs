using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using Routes.Application.Common.Result;

namespace Routes.Application.Queries.GetRoutes
{
    public record GetRoutesQuery : ICommand<Result<GetRoutesResult>>
    {
        public bool? OnlyActive { get; init; }
    }
}