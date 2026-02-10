using Patrimonium.Application.DTOs.Maintenance;

namespace Patrimonium.Application.Interfaces
{
    public interface ICreateMaintenanceUseCase
    {
        Task<Guid> ExecuteAsync(Guid userId, CreateMaintenanceDto dto);
    }
}
