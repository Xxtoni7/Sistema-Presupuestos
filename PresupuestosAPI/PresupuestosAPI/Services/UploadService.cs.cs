using PresupuestosAPI.DTOs.Upload;

namespace PresupuestosAPI.Services
{
    public class UploadService
    {
        private readonly CloudinaryService _cloudinaryService;

        public UploadService(CloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        public async Task<UploadLogoResponseDto> UploadLogoAsync(IFormFile file, string? oldLogoUrl = null)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("Debe seleccionar un archivo.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new Exception("Formato no permitido. Solo se aceptan .jpg, .jpeg, .png y .webp");
            }

            var imageUrl = await _cloudinaryService.UploadImageAsync(file);

            if (!string.IsNullOrWhiteSpace(oldLogoUrl))
            {
                await _cloudinaryService.DeleteImageAsync(oldLogoUrl);
            }

            return new UploadLogoResponseDto
            {
                FileName = Path.GetFileName(imageUrl),
                Url = imageUrl
            };
        }
    }
}