using Patrimonium.Application.DTOs.Dashboard;

namespace Patrimonium.Application.Interfaces
{
    public interface IDashboardQueryService
    {
        Task<DashboardDto> GetDashboard(Guid userId, int month, int year);
    }
}