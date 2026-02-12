using Patrimonium.Application.DTOs.Results;
using Patrimonium.Application.Interfaces;

namespace Patrimonium.Application.Services
{
    public class VacancyEngine : IVacancyEngine
    {
        public async Task<VacancyResult> Calculate(Guid propertyId)
        {
            return await Task.FromResult(new VacancyResult
            {
                PropertyId = propertyId,
                VacancyRate = 0.12m,
                DaysVacant = 40,
                RevenueLoss = 8000m
            });
        }
    }

}
