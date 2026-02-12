using Patrimonium.Application.DTOs.Results;

namespace Patrimonium.Application.Interfaces
{
    public interface IProjectionEngine
    {
        Task<PropertyProjectionResult> Project(Guid propertyId, int months);
    }
}
