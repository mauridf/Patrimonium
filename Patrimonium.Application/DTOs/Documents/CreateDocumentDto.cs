using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Documents
{
    public record CreateDocumentDto(
        Guid? PropertyId,
        Guid? ContractId,
        DocumentType Type,
        string Description
    );
}
