using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation;

namespace Routes.Application.Commands.DeleteRoute
{
    public class DeleteRouteValidator : Validator<DeleteRouteCommand>
    {
        public DeleteRouteValidator()
        {
            RuleFor(x => x.RouteId)
                .NotEmpty().WithMessage("Route ID is required");
        }
    }
}