using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routes.Domain.Entities;

namespace Routes.Domain.Interfaces.Repositories
{
    public interface IReadRouteRepository
    {
        // методы без отслеживания
        Task<Route?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Route>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<Route>> GetActiveRoutesAsync(CancellationToken cancellationToken = default);
    }
}