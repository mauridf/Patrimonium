using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Document : AuditableEntity
    {
        public Guid? PropertyId { get; set; }
        public Guid? ContractId { get; set; }
        public DocumentType Type { get; set; }
        public string FileUrl { get; set; } = default!;
        public string? Description { get; set; }
    }
}
