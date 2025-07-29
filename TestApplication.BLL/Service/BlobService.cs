using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace TestApplication.BLL.Service
{
    public class BlobService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobService(IConfiguration config)
        {
            var connectionString = config["AzureBlobStorage:ConnectionString"];
            var containerName = config["AzureBlobStorage:ContainerName"];
            _containerClient = new BlobContainerClient(connectionString, containerName);
            _containerClient.CreateIfNotExists(); // Optional
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var blobClient = _containerClient.GetBlobClient(file.FileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            return blobClient.Uri.ToString();
        }
    }
}