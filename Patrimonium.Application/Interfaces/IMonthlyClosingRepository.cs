using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IMonthlyClosingRepository
    {
        Task SaveAsync(MonthlyClosing closing);
        Task<MonthlyClosing?> GetByPeriodAsync(Guid userId, int year, int month);
    }
}
