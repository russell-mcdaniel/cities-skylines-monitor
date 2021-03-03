using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Insights.Web.Api.Models;

namespace Insights.Web.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UploadController> _logger;

        public UploadController(IConfiguration configuration, ILogger<UploadController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        //public async Task<IActionResult> Post(string fileName, string fileContent)
        public async Task<IActionResult> Post(LogFile file)
        {
            _logger.LogDebug($"File.FileName:   {file.FileName}");
            _logger.LogDebug($"File.Length:     {file.FileContent.Length}");

            var connectionString = _configuration["Insights:LogFileStorageConnectionString"];
            var serviceClient = new BlobServiceClient(connectionString);

            var containerName = "game";
            var containerClient = serviceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();     // TODO: Create once at startup.

            var playerKey = "fec3ff77-d5a5-456b-974c-5f130879e720";

            var folderDate = GetDateFromFileName(file.FileName);
            var folder = $"{folderDate:yyyyMMdd}";
            var path = $"{playerKey}/{folder}/{file.FileName}";

            var fileBytes = Encoding.UTF8.GetBytes(file.FileContent);
            using var stream = new MemoryStream(fileBytes) { Position = 0 };

            var blob = containerClient.GetBlobClient(path);
            var info = await blob.UploadAsync(stream);

            _logger.LogDebug($"Blob Hash:       {Encoding.Default.GetString(info.Value.ContentHash)}");

            return Ok();
        }

        private DateTime GetDateFromFileName(string fileName)
        {
            // The game log file name format is yyyyMMdd-HHmmss.log.
            var dateText = fileName.Substring(0, 8);
            var date = DateTime.ParseExact(dateText, "yyyyMMdd", CultureInfo.InvariantCulture);

            return date;
        }
    }
}
