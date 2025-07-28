using Microsoft.AspNetCore.Identity;
using TestApplication.BLL.Interface;
using TestApplication.DAL.Interface;
using TestApplication.Models.Entities;
using TestApplication.Models.Models;

namespace TestApplication.BLL.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;

        public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _passwordHasher = new PasswordHasher<ApplicationUser>();
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _authRepository.GetUserByEmailAsync(dto.Email);
            if (existingUser != null)
                return "Email is already registered.";

            var user = new ApplicationUser
            {
                Email = dto.Email,
                FullName = dto.FullName,
                Role = "User"
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            await _authRepository.AddUserAsync(user);

            return "User registered successfully.";
        }

        public async Task<string?> LoginAsync(LoginDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Email and password are required.");

            var user = await _authRepository.GetUserByEmailAsync(dto.Email);
            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                return null;

            return _tokenService.GenerateJwtToken(user);
        }
    }
}