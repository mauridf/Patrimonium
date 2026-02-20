using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly PatrimoniumDbContext _db;

        public MediaRepository(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task Add(Media media)
            => await _db.Media.AddAsync(media);

        public async Task<List<Media>> GetByProperty(Guid propertyId)
            => await _db.Media
                .Where(x => x.PropertyId == propertyId && !x.IsDeleted)
                .ToListAsync();

        public async Task SaveChanges()
            => await _db.SaveChangesAsync();
    }
}