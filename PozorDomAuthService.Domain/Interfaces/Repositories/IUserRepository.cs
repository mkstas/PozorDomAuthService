using PozorDomAuthService.Domain.Models;

namespace PozorDomAuthService.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAync(string email, string passwordHash);
        Task<User?> GetUserByIdAsync(UserId id);
        Task<User?> GetUserByEmailAsync(string email);
        Task UpdateEmailAsync(UserId id, string email);
        Task UpdatePasswordAsync(UserId id, string passwordHash);
    }
}
