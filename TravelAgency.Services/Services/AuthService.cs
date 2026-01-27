using Microsoft.AspNetCore.Identity;
using TravelAgency.Core.Interfaces;
using TravelAgency.Core.Models;
using TravelAgency.Core.DTOs;

namespace TravelAgency.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher<Users> _passwordHasher;

        public AuthService(IUserRepository userRepo, IPasswordHasher<Users> passwordHasher)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
        }

        public async Task<Users?> RegisterAsync(UserRegistrationDto dto)
        {
            var existingUser = await _userRepo.GetByEmailAsync(dto.Email);
            if (existingUser != null) return null;

            var user = new Users
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            return user;
        }

        public async Task<string?> LoginAsync(UserLoginDto dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);
            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed) return null;

            return "dummy-jwt-token";
        }
    }
}