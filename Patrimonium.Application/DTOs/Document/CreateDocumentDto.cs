using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Document
{
    public class CreateDocumentDto
    {
        public Guid? PropertyId { get; set; }
        public Guid? ContractId { get; set; }
        public DocumentType Type { get; set; }
        public string FileUrl { get; set; } = default!;
        public string? Description { get; set; }
    }
}
