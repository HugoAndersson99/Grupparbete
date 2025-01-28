using Application.Interfaces.AzureStorageIntefaces;
using Azure.Storage.Blobs;
using Domain.Models;
using Infrastructure.Databases;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly Database _database;

        public AzureStorageService(BlobServiceClient blobServiceClient, Database database)
        {
            _blobServiceClient = blobServiceClient;
            _database = database;
        }

        public async Task<string> UploadAsync(IFormFile file, string containerName, Guid userId)
        {
            try
            {
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                await blobContainerClient.CreateIfNotExistsAsync();

                var blobClient = blobContainerClient.GetBlobClient(file.FileName);

                await using var stream = file.OpenReadStream();
                await blobClient.UploadAsync(stream, true);

                var fileUrl = blobClient.Uri.ToString();

                // Spara URL:en i databasen kopplad till användarens CV
                var cv = new CV
                {
                    Id = Guid.NewGuid(),
                    Title = file.FileName, // Eller något annat sätt att namnge CV
                    UserId = userId,
                    PdfUrl = fileUrl
                };

                _database.CVs.Add(cv);
                await _database.SaveChangesAsync();


                return fileUrl;
            }
            catch (Exception ex)
            {
                // Logga fel om du vill
                return null;
            }
        }
    }
}
