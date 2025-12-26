using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PozorDomAuthService.Domain.Interfaces.Providers;

namespace PozorDomAuthService.Infrastructure.Providers
{
    public class ImageProvider(
        IWebHostEnvironment environment) : IImageProvider
    {
        private readonly IWebHostEnvironment _environment = environment;
        private readonly string[] allowedImageExtensions = [".jpg", ".jpeg", ".png", ".gif"];

        public async Task<string> SaveSingleImage(IFormFile image, string destination = "uploads")
        {
            ValidateImageFile(image);
            ValidateImageExtension(image.FileName);
            CreateFileDestination(destination);
            var fileName = GenerateFileName(image.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, destination, fileName);
            await SaveFileAsync(image, filePath);

            return $"{destination}/{fileName}";
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

        private void CreateFileDestination(string destination)
        {
            Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, destination));
        }

        private void ValidateImageExtension(string originalFileName)
        {
            var extension = Path.GetExtension(originalFileName).ToLowerInvariant();

            if (!allowedImageExtensions.Contains(extension))
                throw new InvalidDataException("Invalid image format.");
        }

        private static void ValidateImageFile(IFormFile image)
        {
            ArgumentNullException.ThrowIfNull(image);

            if (image.Length == 0)
                throw new ArgumentException("Image file cannot be empty", nameof(image));
        }

        private static string GenerateFileName(string originalFileName)
        {
            var extension = Path.GetExtension(originalFileName).ToLowerInvariant();

            return $"{Guid.NewGuid()}.{extension}";
        }

        private static async Task SaveFileAsync(IFormFile file, string destination)
        {
            var stream = new FileStream(destination, FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }
}
