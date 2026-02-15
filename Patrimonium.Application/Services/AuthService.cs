using Patrimonium.Application.DTOs.Auth;
using Patrimonium.Application.Interfaces;
using Patrimonium.Domain.Entities;

namespace Patrimonium.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtService _jwt;

        public AuthService(IUserRepository userRepo, IJwtService jwt)
        {
            _userRepo = userRepo;
            _jwt = jwt;
        }

        public async Task<string> Register(RegisterDto dto)
        {
            if (await _userRepo.EmailExists(dto.Email))
                throw new Exception("Email já registrado");

            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User(dto.Name, dto.Email, hash, dto.Phone);

            await _userRepo.Add(user);
            await _userRepo.SaveChanges();

            return _jwt.GenerateToken(user);
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _userRepo.GetByEmail(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new Exception("Credenciais inválidas");

            if (!user.IsActive)
                throw new Exception("Usuário inativo");

            return _jwt.GenerateToken(user);
        }
    }
}
