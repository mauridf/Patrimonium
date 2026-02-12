using Patrimonium.Application.DTOs.Results;

namespace Patrimonium.Application.Interfaces
{
    public interface IVacancyEngine
    {
        Task<VacancyResult> Calculate(Guid propertyId);
    }
}
