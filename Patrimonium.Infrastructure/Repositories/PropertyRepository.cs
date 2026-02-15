using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PatrimoniumDbContext _db;

        public PropertyRepository(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task Add(Property property)
            => await _db.Properties.AddAsync(property);

        public async Task<List<Property>> GetByUser(Guid userId)
            => await _db.Properties
                .Where(x => x.UserId == userId && !x.IsDeleted)
                .ToListAsync();

        public async Task<Property?> GetById(Guid id, Guid userId)
            => await _db.Properties
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId && !x.IsDeleted);

        public async Task SaveChanges()
            => await _db.SaveChangesAsync();
    }
}
