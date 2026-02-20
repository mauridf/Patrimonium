using Microsoft.EntityFrameworkCore;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;
using Patrimonium.Infrastructure.Data.Context;

namespace Patrimonium.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly PatrimoniumDbContext _db;

        public DocumentRepository(PatrimoniumDbContext db)
        {
            _db = db;
        }

        public async Task Add(Document document)
            => await _db.Documents.AddAsync(document);

        public async Task<List<Document>> GetByUser(Guid userId)
            => await _db.Documents
                .Where(x => x.UserId == userId && !x.IsDeleted)
                .ToListAsync();

        public async Task SaveChanges()
            => await _db.SaveChangesAsync();
    }
}