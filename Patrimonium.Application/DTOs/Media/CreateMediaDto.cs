using Patrimonium.Domain.Enums;

namespace Patrimonium.Application.DTOs.Media
{
    public record CreateMediaDto(
        Guid PropertyId,
        MediaType Type,
        bool IsCover
    );
}
