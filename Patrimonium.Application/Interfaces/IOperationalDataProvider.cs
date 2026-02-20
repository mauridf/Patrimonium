using Patrimonium.Application.DTOs.Consolidation;

namespace Patrimonium.Application.Interfaces
{
    public interface IOperationalDataProvider
    {
        Task<OperationalDataDto> GetOperationalDataAsync(Guid userId, int year, int month);
    }
}
