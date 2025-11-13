using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routes.Domain.Entities;

namespace Routes.Domain.Interfaces.Repositories
{
    public interface IRouteRepository
    {
        Task<Route?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Route route, CancellationToken cancellationToken = default);
        Task UpdateAsync(Route route, CancellationToken cancellationToken = default);
        Task DeleteAsync(Route route, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}