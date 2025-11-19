using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Routes.API.Endpoints.Routes.GetRouteById
{
    public class GetRouteByIdRequestValidator : Validator<GetRouteByIdRequest>
    {
        public GetRouteByIdRequestValidator()
        {
            RuleFor(x => x.RouteId)
                .NotEmpty().WithMessage("Route Id is required");
        }
    }
}