using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Routes.Domain.Interfaces.Repositories;
using Routes.Infrastructure.Data;
using Routes.Infrastructure.Data.Repositories;

namespace Routes.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IReadRouteRepository, ReadRouteRepository>();

            return services;
        }
    }
}

