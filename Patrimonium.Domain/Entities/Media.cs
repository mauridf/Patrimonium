using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Media : AuditableEntity
    {
        public Guid PropertyId { get; set; }
        public MediaType Type { get; set; }
        public string Url { get; set; } = default!;
        public bool IsCover { get; set; }

        public Property Property { get; set; } = default!;
    }
}
