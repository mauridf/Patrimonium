using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IMaintenanceRepository
    {
        Task Add(Maintenance maintenance);
        Task<Maintenance?> GetById(Guid id, Guid userId);
        Task<List<Maintenance>> GetByUser(Guid userId);
        Task SaveChanges();
    }
}