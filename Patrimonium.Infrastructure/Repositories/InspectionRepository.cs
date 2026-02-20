using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Repositories
{
    public class InspectionRepository : IInspectionRepository
    {
        private readonly PatrimoniumDbContext _db;

        public InspectionRepository(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task Add(Inspection inspection)
            => await _db.Inspections.AddAsync(inspection);

        public async Task<List<Inspection>> GetByProperty(Guid propertyId)
            => await _db.Inspections
                .Where(x => x.PropertyId == propertyId && !x.IsDeleted)
                .ToListAsync();

        public async Task<Inspection?> GetById(Guid id)
            => await _db.Inspections
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        public async Task SaveChanges()
            => await _db.SaveChangesAsync();
    }
}