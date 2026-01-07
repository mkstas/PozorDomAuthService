using PozorDomAuthService.Domain.Entities;
using PozorDomAuthService.Domain.Interfaces.Repositories;
using PozorDomAuthService.Domain.Interfaces.Services;
using PozorDomAuthService.Infrastructure.Exceptions;
using PozorDomAuthService.Infrastructure.Providers.Images;
using PozorDomAuthService.Infrastructure.Providers.Jwt;

namespace PozorDomAuthService.Application.Services
{
    public class UserService(
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        IImageProvider imageProvider) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IImageProvider _imageProvider = imageProvider;

        public async Task<string> LoginOrRegisterAsync(string phoneNumber)
        {
            var user = await _userRepository.GetUserByPhoneNumberAsync(phoneNumber)
                ?? await RegisterNewUserAsync(phoneNumber);

            return _jwtProvider.GenerateToken(user);
        }

        private async Task<UserEntity> RegisterNewUserAsync(string phoneNumber)
        {
            await _userRepository.CreateUserAsync(phoneNumber);

            var user = await _userRepository.GetUserByPhoneNumberAsync(phoneNumber);

            return user ?? throw new InternalServerException("Register user failed.");
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            return user ?? throw new NotFoundException("User not found.");
        }

        public async Task UpdateUserPhoneNumberAsync(Guid userId, string phoneNumber)
        {
            var rowsAffected = await _userRepository.UpdatePhoneNumberAsync(userId, phoneNumber);

            if (rowsAffected == 0)
                throw new NotFoundException("User not found.");
        }

        public async Task UpdateUserInfoAsync(Guid userId, string fullName)
        {
            var rowsAffected = await _userRepository.UpdateInfoAsync(userId, fullName);

            if (rowsAffected == 0)
                throw new NotFoundException("User not found.");
        }

        public async Task UpdateUserEmailAsync(Guid userId, string email)
        {
            var rowsAffected = await _userRepository.UpdateEmailAsync(userId, email);

            if (rowsAffected == 0)
                throw new NotFoundException("User not found.");
        }

        public async Task UpdateUserImageUrlAsync(Guid userId, Stream imageStream, string originalName)
        {
            var user = await _userRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException("User not found");

            if (user.ImageUrl != "")
                await _imageProvider.DeleteSingleImage(user.ImageUrl);

            var imageUrl = await _imageProvider.SaveSingleImage(imageStream, originalName);

            await _userRepository.UpdateImageUrlAsync(userId, imageUrl);
        }
    }
}
