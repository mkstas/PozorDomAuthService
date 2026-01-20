using PozorDomAuthService.Domain.Models;

namespace PozorDomAuthService.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAync(string email, string passwordHash);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(string email);
        Task UpdateEmailAsync(Guid id, string email);
        Task UpdatePasswordHashAsync(Guid id, string passwordHash);
    }
}
