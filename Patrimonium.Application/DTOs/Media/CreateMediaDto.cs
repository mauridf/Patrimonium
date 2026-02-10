using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Media
{
    public class CreateMediaDto
    {
        public Guid PropertyId { get; set; }
        public MediaType Type { get; set; }
        public string Url { get; set; } = default!;
        public bool IsCover { get; set; }
    }
}
