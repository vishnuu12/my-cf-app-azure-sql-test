using Microsoft.AspNetCore.Mvc;
using TestApplication.BLL.Service;

namespace TestApplication.API.Controllers
{
    public class FilesController : ControllerBase
    {
        private readonly BlobService _blobService;

        public FilesController(BlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var blobUrl = await _blobService.UploadFileAsync(file);
            return Ok(new { Url = blobUrl });
        }
    }
}
