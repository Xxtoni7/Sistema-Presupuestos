using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using PresupuestosAPI.Settings;

namespace PresupuestosAPI.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account)
            {
                Api = { Secure = true }
            };
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            await using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "presupuestos/companies/logos",
                UseFilename = false,
                UniqueFilename = true,
                Overwrite = false
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }

            return uploadResult.SecureUrl.ToString();
        }

        public async Task DeleteImageAsync(string? imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return;
            }

            var publicId = GetPublicIdFromUrl(imageUrl);

            if (string.IsNullOrWhiteSpace(publicId))
            {
                return;
            }

            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);

            if (result.Error != null)
            {
                throw new Exception(result.Error.Message);
            }
        }

        private static string? GetPublicIdFromUrl(string imageUrl)
        {
            var uploadIndex = imageUrl.IndexOf("/upload/", StringComparison.OrdinalIgnoreCase);

            if (uploadIndex == -1)
            {
                return null;
            }

            var pathAfterUpload = imageUrl[(uploadIndex + "/upload/".Length)..];

            var parts = pathAfterUpload.Split('/').ToList();

            if (parts.Count == 0)
            {
                return null;
            }

            if (parts[0].StartsWith("v") && parts[0].Skip(1).All(char.IsDigit))
            {
                parts.RemoveAt(0);
            }

            var publicIdWithExtension = string.Join("/", parts);
            var publicId = Path.ChangeExtension(publicIdWithExtension, null);

            return publicId;
        }
    }
}