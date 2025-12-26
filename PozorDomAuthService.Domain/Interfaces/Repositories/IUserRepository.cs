using PozorDomAuthService.Domain.Entities;

namespace PozorDomAuthService.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(string phoneNumber);
        Task<UserEntity?> GetUserByIdAsync(Guid userId);
        Task<UserEntity?> GetUserByPhoneNumberAsync(string phoneNumber);
        Task<int> UpdatePhoneNumberAsync(Guid userId, string phoneNumber);
        Task<int> UpdateInfoAsync(Guid userId, string fullName);
        Task<int> UpdateEmailAsync(Guid userId, string email);
        Task<int> UpdateImageUrlAsync(Guid userId, string imageUrl);
    }
}
