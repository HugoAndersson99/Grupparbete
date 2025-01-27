using Application.Interfaces.AzureStorageIntefaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AzureStorageController : Controller
    {
        private readonly IAzureStorageService _azureStorageService;

        public AzureStorageController(IAzureStorageService azureStorageService)
        {
            _azureStorageService = azureStorageService;
        }

        [HttpPost("upload-cv")]
        public async Task<IActionResult> UploadCv(IFormFile file, Guid userId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required.");

            if (userId == Guid.Empty)
                return BadRequest("UserId is required.");

            var containerName = "cv-container";

            var fileUrl = await _azureStorageService.UploadAsync(file, containerName, userId);

            if (fileUrl == null)
                return StatusCode(500, "An error occurred while uploading the file.");

            return Ok(new { Url = fileUrl });
        }
    }
}
