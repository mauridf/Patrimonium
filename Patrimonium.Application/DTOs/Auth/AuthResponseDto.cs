namespace Patrimonium.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = default!;
        public DateTime Expiration { get; set; }
    }
}
