using Application.Interfaces.AzureStorageIntefaces;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Configurations
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> UploadAsync(IFormFile file, string containerName)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                await blobContainerClient.CreateIfNotExistsAsync();

                var blobClient = blobContainerClient.GetBlobClient(file.FileName);

                await using var stream = file.OpenReadStream();
                await blobClient.UploadAsync(stream, true);

                return blobClient.Uri.ToString();
            }
            catch (Exception ex)
            {
                // Logga fel om du vill
                return null;
            }
        }
    }
}
