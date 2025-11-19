using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Routes.API.Endpoints.Routes.DeleteRoute
{
    public class DeleteRouteRequestValidator : Validator<DeleteRouteRequest>
    {
        public DeleteRouteRequestValidator()
        {
            RuleFor(x => x.RouteId)
                .NotEmpty().WithMessage("Route Id is required");
        }
    }
}