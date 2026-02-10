using Patrimonium.Application.DTOs.Property;

namespace Patrimonium.Application.Interfaces
{
    public interface ICreatePropertyUseCase
    {
        Task<Guid> ExecuteAsync(Guid userId, CreatePropertyDto dto);
    }
}
