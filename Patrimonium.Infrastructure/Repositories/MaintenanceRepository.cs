using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Repositories
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        private readonly PatrimoniumDbContext _db;

        public MaintenanceRepository(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task Add(Maintenance maintenance)
            => await _db.Maintenances.AddAsync(maintenance);

        public async Task<Maintenance?> GetById(Guid id, Guid userId)
            => await _db.Maintenances
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId && !x.IsDeleted);

        public async Task<List<Maintenance>> GetByUser(Guid userId)
            => await _db.Maintenances
                .Where(x => x.UserId == userId && !x.IsDeleted)
                .ToListAsync();

        public async Task SaveChanges()
            => await _db.SaveChangesAsync();
    }
}