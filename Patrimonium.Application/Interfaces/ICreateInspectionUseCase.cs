using Patrimonium.Application.DTOs.Inspection;

namespace Patrimonium.Application.Interfaces
{
    public interface ICreateInspectionUseCase
    {
        Task<Guid> ExecuteAsync(Guid userId, CreateInspectionDto dto);
    }
}
