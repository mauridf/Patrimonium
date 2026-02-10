using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Document
{
    public class UpdateDocumentDto
    {
        public DocumentType Type { get; set; }
        public string FileUrl { get; set; } = default!;
        public string? Description { get; set; }
    }
}
