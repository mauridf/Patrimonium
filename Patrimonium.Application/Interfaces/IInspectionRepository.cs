using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IInspectionRepository
    {
        Task Add(Inspection inspection);
        Task<List<Inspection>> GetByProperty(Guid propertyId);
        Task<Inspection?> GetById(Guid id);
        Task SaveChanges();
    }
}