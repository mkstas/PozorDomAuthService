using Microsoft.EntityFrameworkCore;
using PozorDomAuthService.Api.Extensions;
using PozorDomAuthService.Application.Services;
using PozorDomAuthService.Domain.Interfaces.Repositories;
using PozorDomAuthService.Domain.Interfaces.Services;
using PozorDomAuthService.Infrastructure.Providers.Images;
using PozorDomAuthService.Infrastructure.Providers.Jwt;
using PozorDomAuthService.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddImageConfiguration(builder.Configuration);
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddApiAuthentification(builder.Configuration);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IImageProvider, ImageProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseGlobalExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapGroup("api/v1").WithTags("v1").MapControllers();
app.Run();
