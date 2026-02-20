using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Document : BaseEntity
    {
        public Guid UserId { get; private set; }

        public Guid? PropertyId { get; private set; }
        public Guid? ContractId { get; private set; }

        public DocumentType Type { get; private set; }
        public string FileUrl { get; private set; } = null!;
        public string Description { get; private set; } = null!;

        protected Document() { }

        public Document(
            Guid userId,
            Guid? propertyId,
            Guid? contractId,
            DocumentType type,
            string fileUrl,
            string description
        )
        {
            UserId = userId;
            PropertyId = propertyId;
            ContractId = contractId;
            Type = type;
            FileUrl = fileUrl;
            Description = description;
        }
    }
}