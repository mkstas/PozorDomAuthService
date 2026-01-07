
namespace PozorDomAuthService.Infrastructure.Providers.Images
{
    public interface IImageProvider
    {
        Task<string> SaveSingleImage(Stream imageStream, string originalName);
        Task DeleteSingleImage(string destination);
    }
}
