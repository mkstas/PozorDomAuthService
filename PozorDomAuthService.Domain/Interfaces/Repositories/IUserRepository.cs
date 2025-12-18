using PozorDomAuthService.Domain.Entities;

namespace PozorDomAuthService.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(string phoneNumber);
        Task<UserEntity?> GetByIdAsync(Guid id);
        Task<UserEntity?> GetByPhoneNumberAsync(string phoneNumber);
        Task<int> UpdatePhoneNumberAsync(Guid id, string phoneNumber);
        Task<int> UpdateInfoAsync(Guid id, string fullName);
        Task<int> UpdateEmailAsync(Guid id, string email);
        Task<int> UpdateImageUrlAsync(Guid id, string imageUrl);
    }
}
