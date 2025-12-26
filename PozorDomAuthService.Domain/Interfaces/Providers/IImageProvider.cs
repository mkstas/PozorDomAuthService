using Microsoft.AspNetCore.Http;

namespace PozorDomAuthService.Domain.Interfaces.Providers
{
    public interface IImageProvider
    {
        Task<string> SaveSingleImage(IFormFile image, string destination = "uploads");
    }
}
