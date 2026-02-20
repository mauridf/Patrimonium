using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IDocumentRepository
    {
        Task Add(Document document);
        Task<List<Document>> GetByUser(Guid userId);
        Task SaveChanges();
    }
}