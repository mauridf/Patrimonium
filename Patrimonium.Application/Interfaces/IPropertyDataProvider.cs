using Patrimonium.Application.DTOs.Consolidation;

namespace Patrimonium.Application.Interfaces
{
    public interface IPropertyDataProvider
    {
        Task<IReadOnlyCollection<PropertyDto>> GetPropertiesAsync(Guid userId);
    }
}
