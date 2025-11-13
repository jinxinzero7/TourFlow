using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Routes.Domain.Entities;
using Routes.Domain.Interfaces.Repositories;
using Routes.Infrastructure.Data;

namespace Routes.Infrastructure.Data.Repositories
{
    public class ReadRouteRepository : IReadRouteRepository
    {
        private readonly ApplicationDbContext _context;

        public ReadRouteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Route?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Routes
                .AsNoTracking()
                .Include(r => r.Locations)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<List<Route>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Routes
                .AsNoTracking()
                .Include(r => r.Locations)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Route>> GetActiveRoutesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Routes
                .AsNoTracking()
                .Include(r => r.Locations)
                .Where(r => r.IsActive)
                .ToListAsync(cancellationToken);
        }
    }
}