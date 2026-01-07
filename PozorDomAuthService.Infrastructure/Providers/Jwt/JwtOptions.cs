namespace PozorDomAuthService.Infrastructure.Providers.Jwt
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiresDays { get; set; } = 0;
        public string CookieName { get; set; } = string.Empty;
    }
}
