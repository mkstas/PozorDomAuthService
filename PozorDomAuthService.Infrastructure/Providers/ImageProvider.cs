using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PozorDomAuthService.Domain.Interfaces.Providers;

namespace PozorDomAuthService.Infrastructure.Providers
{
    public class ImageProvider(
        IWebHostEnvironment environment) : IImageProvider
    {
        private readonly IWebHostEnvironment _environment = environment;
        private readonly string[] allowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        public async Task<string> SaveSingleImage(IFormFile image, string destination = "uploads")
        {
            ArgumentNullException.ThrowIfNull(image);

            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (!allowedImageExtensions.Contains(extension))
                throw new InvalidDataException("Invalid image format.");

            var folder = Path.Combine(_environment.WebRootPath, destination);
            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(folder, fileName);

            Directory.CreateDirectory(folder);

            using (var stream = new FileStream(filePath, FileMode.Create))
                await image.CopyToAsync(stream);

            return "uploads/" + fileName;
        }

        public Task DeleteSingleImage(string destination)
        {
            if (string.IsNullOrEmpty(destination))
                return Task.CompletedTask;

            var imagePath = Path.Combine(_environment.WebRootPath, destination);

            if (File.Exists(imagePath))
                File.Delete(imagePath);

            return Task.CompletedTask;
        }
    }
}
