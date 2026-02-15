using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IPropertyRepository
    {
        Task Add(Property property);
        Task<List<Property>> GetByUser(Guid userId);
        Task<Property?> GetById(Guid id, Guid userId);
        Task SaveChanges();
    }
}
