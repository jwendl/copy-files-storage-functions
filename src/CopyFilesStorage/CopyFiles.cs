using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace CopyFilesStorage
{
    public class CopyFiles
    {
        [FunctionName("CopyFiles")]
        public async Task Run([BlobTrigger("files/{name}", Connection = "SourceStorageAccount")] Stream inputStream, [Blob("outputs/{name}", FileAccess.Write, Connection = "DestinationStorageAccount")] Stream outputStream, [Blob("files/{name}", Connection = "SourceStorageAccount")] BlobContainerClient sourceBlobContainerClient, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {inputStream.Length} Bytes");

            await inputStream.CopyToAsync(outputStream);

            await sourceBlobContainerClient.DeleteBlobIfExistsAsync(name);
        }
    }
}
