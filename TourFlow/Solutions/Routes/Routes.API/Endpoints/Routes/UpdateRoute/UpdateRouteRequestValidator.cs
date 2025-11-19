using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Routes.API.Endpoints.Routes.UpdateRoute
{
    public class UpdateRouteRequestValidator : Validator<UpdateRouteRequest>
    {
        public UpdateRouteRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Route name is required")
                .MinimumLength(3).WithMessage("Route name must be at least 3 characters")
                .MaximumLength(100).WithMessage("Route name must not exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

            RuleFor(x => x.BasePrice)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.DurationDays)
                .GreaterThan(0).WithMessage("Duration must be at least 1 day");

            RuleForEach(x => x.Locations)
                .ChildRules(location =>
                {
                    location.RuleFor(x => x.Location)
                        .NotEmpty().WithMessage("Location name is required")
                        .MaximumLength(100).WithMessage("Location name must not exceed 100 characters");

                    location.RuleFor(x => x.StayDurationDays)
                        .GreaterThan(0).WithMessage("Stay duration must be at least 1 day");
                });
        }
    }
}