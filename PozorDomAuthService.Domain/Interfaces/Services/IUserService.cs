using PozorDomAuthService.Domain.Entities;

namespace PozorDomAuthService.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> LoginOrRegisterAsync(string phoneNumber);
        Task<UserEntity> GetUserByIdAsync(Guid userId);
    }
}
