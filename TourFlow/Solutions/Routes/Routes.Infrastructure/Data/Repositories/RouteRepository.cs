using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Routes.Infrastructure.Data;
using Routes.Domain.Interfaces.Repositories;
using Routes.Domain.Entities;

namespace Routes.Infrastructure.Data.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly ApplicationDbContext _context;

        public RouteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Route?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Routes
                .Include(r => r.Locations)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task AddAsync(Route route, CancellationToken cancellationToken = default)
        {
            await _context.Routes.AddAsync(route, cancellationToken);
        }

        public Task UpdateAsync(Route route, CancellationToken cancellationToken = default)
        {
            _context.Routes.Update(route);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Route route, CancellationToken cancellationToken = default)
        {
            _context.Routes.Remove(route);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Routes.AnyAsync(r => r.Id == id, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}