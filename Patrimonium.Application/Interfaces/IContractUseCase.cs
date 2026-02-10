using Patrimonium.Application.DTOs.Contract;

namespace Patrimonium.Application.Interfaces
{
    public interface ICreateContractUseCase
    {
        Task<Guid> ExecuteAsync(Guid userId, CreateContractDto dto);
    }
}
