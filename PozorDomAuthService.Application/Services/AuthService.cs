using PozorDomAuthService.Domain.Interfaces.Repositories;
using PozorDomAuthService.Domain.Interfaces.Services;
using PozorDomAuthService.Infrastructure.Providers.Jwt;
using PozorDomAuthService.Infrastructure.Shared;

namespace PozorDomAuthService.Application.Services
{
    public class AuthService(
        IUserRepository userRepository,
        IJwtProvider jwtProvider) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email) 
                ?? throw new UnauthorizedAccessException("Incorrect email address or password.");

            if (!PasswordHasher.Verify(password, user.Password.Hash))
            {
                throw new UnauthorizedAccessException("Incorrect email address or password.");
            }

            return _jwtProvider.GenerateToken(user.Id.Value, user.Email.Address);
        }

        public async Task Register(string email, string password)
        {
            var passwordHash = PasswordHasher.Generate(password);
            await _userRepository.CreateUserAync(email, passwordHash);
        }
    }
}
