namespace PozorDomAuthService.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task Register(string email, string password);
        Task<string> Login(string email, string password);
    }
}
