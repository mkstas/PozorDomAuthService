using PozorDomAuthService.Domain.Interfaces.Repositories;
using PozorDomAuthService.Domain.Interfaces.Services;
using PozorDomAuthService.Domain.Models;
using PozorDomAuthService.Domain.Shared.Exceptions;
using PozorDomAuthService.Infrastructure.Shared;

namespace PozorDomAuthService.Application.Services
{
    public class UserService(
        IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User> GetUserByIdAsync(UserId id)
        {
            return await _userRepository.GetUserByIdAsync(id)
                ?? throw new NotFoundException("User with this email address does not exist.");
        }

        public async Task ChangeEmailAsync(UserId id, string email)
        {
            await _userRepository.UpdateEmailAsync(id, email);
        }

        public async Task ChangePasswordAsync(UserId id, string password, string newPassword)
        {
            var user = await _userRepository.GetUserByIdAsync(id)
                ?? throw new NotFoundException("User with this email address does not exist.");

            if (!PasswordHasher.Verify(password, user.Password.Hash))
            {
                throw new UnauthorizedAccessException("Current password is incorrect.");
            }

            await _userRepository.UpdatePasswordAsync(id, newPassword);
        }
    }
}
