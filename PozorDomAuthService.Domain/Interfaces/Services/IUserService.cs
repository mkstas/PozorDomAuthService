using PozorDomAuthService.Domain.Models;

namespace PozorDomAuthService.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(UserId id);
        Task ChangeEmailAsync(UserId id, string email);
        Task ChangePasswordAsync(UserId id, string password, string newPassword);
    }
}
