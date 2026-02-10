using Patrimonium.Application.DTOs.Dashboard;

namespace Patrimonium.Application.Interfaces
{
    public interface IDashboardUseCase
    {
        Task<DashboardSummaryDto> GetSummaryAsync(Guid userId);
    }
}
