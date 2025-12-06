namespace PozorDomAuthService.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> LoginOrRegisterAsync(string phoneNumber);
    }
}
