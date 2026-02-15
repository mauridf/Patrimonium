namespace Patrimonium.Application.DTOs.Auth
{
    public record RegisterDto(string Name, string Email, string Password, string? Phone);
}
