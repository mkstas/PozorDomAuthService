using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PozorDomAuthService.Api.Middlewares;
using PozorDomAuthService.Infrastructure.Providers.Jwt;
using PozorDomAuthService.Persistence;
using System.Security.Claims;
using System.Text;

namespace PozorDomAuthService.Api.Extensions
{
    public static class ApiExtensions
    {
        public static void UseGlobalExceptionHandler(
            this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalExceptionHandler>();
        }

        public static void AddCorsConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>()
                ?? throw new InvalidOperationException("AllowedOrigins not configured.");

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
        }

        public static void AddDatabaseContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(nameof(PozorDomAuthServiceDbContext))
                ?? throw new InvalidOperationException("Database connection string not configured.");

            services.AddDbContext<PozorDomAuthServiceDbContext>(
                options =>
                {
                    options.UseNpgsql(connectionString);
                });
        }

        public static void AddJwtConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions))
                ?? throw new InvalidOperationException("JwtOptions not configured.");

            services.Configure<JwtOptions>(jwtOptions);
        }

        public static void AddApiAuthentification(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>()
                ?? throw new InvalidOperationException("JwtOptions not configured.");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies[jwtOptions.CookieName];

                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("User Id claim not found.");

            return Guid.Parse(userIdClaim.Value);
        }
    }
}
