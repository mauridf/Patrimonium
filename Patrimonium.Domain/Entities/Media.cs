using Patrimonium.Domain.Enums;

namespace Patrimonium.Domain.Entities
{
    public class Media : BaseEntity
    {
        public Guid PropertyId { get; private set; }

        public MediaType Type { get; private set; }
        public string Url { get; private set; } = null!;
        public bool IsCover { get; private set; }

        protected Media() { }

        public Media(Guid propertyId, MediaType type, string url, bool isCover)
        {
            PropertyId = propertyId;
            Type = type;
            Url = url;
            IsCover = isCover;
        }

        public void SetCover()
        {
            IsCover = true;
            MarkUpdated();
        }
    }
}