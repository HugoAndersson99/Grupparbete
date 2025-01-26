

using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.AzureStorageIntefaces
{
    public interface IAzureStorageService
    {
        Task<string> UploadAsync(IFormFile file, string containerName);
    }
}
