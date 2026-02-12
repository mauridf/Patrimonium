using Patrimonium.Application.DTOs.Results;

namespace Patrimonium.Application.Interfaces
{
    public interface IValuationEngine
    {
        Task<PropertyValuation> Calculate(Guid propertyId);
    }
}
