namespace Patrimonium.Application.DTOs.Auth
{
    public class RegisterRequestDto
    {
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
