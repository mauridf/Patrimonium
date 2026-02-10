using Patrimonium.Domain.Entities;

namespace Patrimonium.Domain.Services
{
    public class MediaDomainService
    {
        public void Validate(Media media)
        {
            if (media.PropertyId == Guid.Empty)
                throw new Exception("Imóvel obrigatório");

            if (string.IsNullOrWhiteSpace(media.Url))
                throw new Exception("URL da mídia obrigatória");
        }
    }
}
