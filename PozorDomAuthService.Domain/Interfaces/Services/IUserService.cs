using PozorDomAuthService.Domain.Models;

namespace PozorDomAuthService.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task ChangeEmailAsync(Guid id, string email);
        Task ChangePasswordAsync(Guid id, string password, string newPassword);
    }
}
