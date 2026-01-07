using PozorDomAuthService.Domain.Entities;

namespace PozorDomAuthService.Infrastructure.Providers.Jwt
{
    public interface IJwtProvider
    {
        string GenerateToken(UserEntity user);
    }
}
