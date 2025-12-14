using PozorDomAuthService.Domain.Entities;
using PozorDomAuthService.Domain.Interfaces.Providers;
using PozorDomAuthService.Domain.Interfaces.Repositories;
using PozorDomAuthService.Domain.Interfaces.Services;
using PozorDomAuthService.Infrastructure.Exceptions;

namespace PozorDomAuthService.Application.Services
{
    public class UserService(
        IUserRepository userRepository,
        IJwtProvider jwtProvider) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<string> LoginOrRegisterAsync(string phoneNumber)
        {
            var user = await _userRepository.GetByPhoneNumberAsync(phoneNumber)
                ?? await RegisterNewUserAsync(phoneNumber);

            return _jwtProvider.GenerateToken(user);
        }

        private async Task<UserEntity> RegisterNewUserAsync(string phoneNumber)
        {
            await _userRepository.CreateAsync(phoneNumber);

            var user = await _userRepository.GetByPhoneNumberAsync(phoneNumber);

            return user ?? throw new InternalServerException("Register user failed.");
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            return user ?? throw new NotFoundException("User not found.");
        }
    }
}
