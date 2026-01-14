using PozorDomAuthService.Domain.Entities;

namespace PozorDomAuthService.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> LoginOrRegisterAsync(string phoneNumber);
        Task<UserEntity> GetUserByIdAsync(Guid userId);
        Task UpdateUserPhoneNumberAsync(Guid userId, string phoneNumber);
        Task UpdateUserInfoAsync(Guid userId, string fullName);
        Task UpdateUserEmailAsync(Guid userId, string email);
    }
}
