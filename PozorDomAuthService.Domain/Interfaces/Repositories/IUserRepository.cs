using PozorDomAuthService.Domain.Entities;

namespace PozorDomAuthService.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(string phoneNumber);
        Task<UserEntity?> GetByIdAsync(Guid id);
        Task<UserEntity?> GetByPhoneNumberAsync(string phoneNumber);
        Task<int> UpdateAsync(Guid id, string fullName, string imageUrl);
    }
}
