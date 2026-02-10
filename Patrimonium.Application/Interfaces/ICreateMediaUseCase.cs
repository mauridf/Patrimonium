using Patrimonium.Application.DTOs.Media;

namespace Patrimonium.Application.Interfaces
{
    public interface ICreateMediaUseCase
    {
        Task<Guid> ExecuteAsync(Guid userId, CreateMediaDto dto);
    }
}
