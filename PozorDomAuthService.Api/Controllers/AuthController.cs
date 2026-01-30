using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PozorDomAuthService.Api.Contracts;
using PozorDomAuthService.Api.Extensions;
using PozorDomAuthService.Domain.Interfaces.Services;
using PozorDomAuthService.Infrastructure.Providers.Jwt;

namespace PozorDomAuthService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(
        IAuthService authService,
        IOptions<JwtOptions> jwtOptions) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var accessToken = await _authService.Login(request.EmailAddress, request.Password);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            };
            HttpContext.Response.Cookies.Append(_jwtOptions.CookieName, accessToken, cookieOptions);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            await _authService.Register(request.EmailAddress, request.Password);

            return NoContent();
        }
        
        [HttpDelete("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete(_jwtOptions.CookieName);

            return NoContent();
        }

        [HttpGet("validate")]
        [Authorize]
        public async Task<IActionResult> Validate()
        {
            HttpContext.Response.Headers.Append("X-User-Id", User.GetUserId().ToString());

            return NoContent();
        }
    }
}
