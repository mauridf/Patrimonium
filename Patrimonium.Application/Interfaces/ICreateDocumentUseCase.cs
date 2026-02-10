using Patrimonium.Application.DTOs.Document;

namespace Patrimonium.Application.Interfaces
{
    public interface ICreateDocumentUseCase
    {
        Task<Guid> ExecuteAsync(Guid userId, CreateDocumentDto dto);
    }
}
