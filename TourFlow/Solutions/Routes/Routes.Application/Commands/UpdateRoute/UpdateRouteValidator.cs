using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using FluentValidation;

namespace Routes.Application.Commands.UpdateRoute
{
    public class UpdateRouteValidator : Validator<UpdateRouteCommand>
    {
        public UpdateRouteValidator()
        {
            RuleFor(x => x.RouteId)
                .NotEmpty().WithMessage("Route ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Route name is required")
                .MinimumLength(3).WithMessage("Route name must be at least 3 characters")
                .MaximumLength(100).WithMessage("Route name must not exceed 100 characters");

            RuleFor(x => x.BasePrice)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.DurationDays)
                .GreaterThan(0).WithMessage("Duration must be at least 1 day");
        }
    }
}