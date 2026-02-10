using Patrimonium.Application.DTOs.Person;

namespace Patrimonium.Application.Interfaces
{
    public interface ICreatePersonUseCase
    {
        Task<Guid> ExecuteAsync(Guid userId, CreatePersonDto dto);
    }
}
