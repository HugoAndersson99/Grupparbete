using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadPDFController : Controller
    {
        private readonly BlobSettings _blobSettings;
        private readonly ILogger<UploadPDFController> _logger;

        public UploadPDFController(IOptions<BlobSettings> blobSettings, ILogger<UploadPDFController> logger)
        {
            _blobSettings = blobSettings.Value; 
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            _logger.LogInformation("Starting file upload.");

            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("No file uploaded or file is empty.");
                return BadRequest("No file uploaded.");
            }

            try
            {
                // Create BlobServiceClient using connection string from _blobSettings
                _logger.LogInformation("Creating BlobServiceClient.");
                var blobServiceClient = new BlobServiceClient(_blobSettings.AzureBlobStorage);

                // Get or create the container
                _logger.LogInformation("Retrieving or creating container '{ContainerName}'.", _blobSettings.ContainerName);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(_blobSettings.ContainerName);

                // Ensure the container exists
                await blobContainerClient.CreateIfNotExistsAsync();

                // Generate a unique filename for the blob
                var blobFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                _logger.LogInformation("Generated unique filename: {BlobFileName}.", blobFileName);

                // Upload the file
                var blobClient = blobContainerClient.GetBlobClient(blobFileName);

                using (var stream = file.OpenReadStream())
                {
                    _logger.LogInformation("Uploading the file to Blob Storage.");
                    await blobClient.UploadAsync(stream, overwrite: true);
                }

                _logger.LogInformation("File upload completed. Blob URL: {BlobUrl}", blobClient.Uri.ToString());
                return Ok(new { Message = "File uploaded successfully!", BlobUrl = blobClient.Uri.ToString() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during file upload.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
    }
}
