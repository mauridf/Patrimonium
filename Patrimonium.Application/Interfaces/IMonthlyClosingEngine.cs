using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Interfaces
{
    public interface IMonthlyClosingEngine
    {
        Task<MonthlyClosing> GenerateAsync(Guid userId, int year, int month);
    }
}
