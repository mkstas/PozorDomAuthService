namespace PozorDomAuthService.Infrastructure.Providers.Jwt
{
    public interface IJwtProvider
    {
        string GenerateToken(Guid id, string email);
    }
}
