using Patrimonium.Application.DTOs.Property;

namespace Patrimonium.Application.UseCases.Properties
{
    public interface ICreatePropertyUseCase
    {
        Task<Guid> ExecuteAsync(Guid userId, CreatePropertyDto dto);
    }
}
