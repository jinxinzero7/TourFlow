using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Routes.Application.Commands.CreateRoute;
using Routes.Application.Commands.DeleteRoute;
using Routes.Application.Commands.UpdateRoute;
using Routes.Application.Common.Result;
using Routes.Application.Queries.GetRouteById;
using Routes.Application.Queries.GetRoutes;


namespace Routes.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateRouteCommand, Result<CreateRouteResult>>, CreateRouteCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateRouteCommand, Result<UpdateRouteResult>>, UpdateRouteCommandHandler>();
            services.AddScoped<ICommandHandler<DeleteRouteCommand, Result<DeleteRouteResult>>, DeleteRouteCommandHandler>();
            services.AddScoped<ICommandHandler<GetRouteByIdQuery, Result<GetRouteByIdResult>>, GetRouteByIdQueryHandler>();
            services.AddScoped<ICommandHandler<GetRoutesQuery, Result<GetRoutesResult>>, GetRoutesQueryHandler>();
            return services;
        }
    }
}

