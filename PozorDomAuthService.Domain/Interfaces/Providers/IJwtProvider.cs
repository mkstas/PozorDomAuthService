using PozorDomAuthService.Domain.Entities;

namespace PozorDomAuthService.Domain.Interfaces.Providers
{
    public interface IJwtProvider
    {
        string GenerateToken(UserEntity user);
    }
}
