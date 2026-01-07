using Microsoft.Extensions.Options;

namespace PozorDomAuthService.Infrastructure.Providers.Images
{
    public class ImageProvider(IOptions<ImageOptions> options) : IImageProvider
    {
        private readonly ImageOptions _options = options.Value;
        private readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png"];

        public async Task<string> SaveSingleImage(Stream imageStream, string originalName)
        {
            ArgumentNullException.ThrowIfNull(imageStream);
            
            var extension = Path.GetExtension(originalName);

            if (!_allowedExtensions.Contains(extension))
                throw new InvalidDataException("Invalid image format.");

            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(_options.Path, fileName);

            using var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            await stream.CopyToAsync(stream);

            return fileName;
        }

        public Task DeleteSingleImage(string path)
        {
            if (File.Exists(path))
                File.Delete(path);

            return Task.CompletedTask;
        }
    }
}
