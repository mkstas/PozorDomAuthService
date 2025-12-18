using PozorDomAuthService.Domain.Entities;

namespace PozorDomAuthService.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> LoginOrRegisterAsync(string phoneNumber);
        Task<UserEntity> GetUserByIdAsync(Guid userId);
        Task UpdateUserPhoneNumberAsync(Guid id, string phoneNumber);
        Task UpdateUserInfoAsync(Guid id, string fullName);
        Task UpdateUserEmailAsync(Guid id, string email);
        Task UpdateUserImageUrlAsync(Guid id, string imageUrl);
    }
}
