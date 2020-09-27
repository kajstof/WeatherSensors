using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using BlobInfo = WeatherSensors.Api.Models.BlobInfo;

namespace WeatherSensors.Api.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobContainerClient _blobContainerClient;

        public BlobService(BlobContainerClient blobContainerClient)
        {
            _blobContainerClient = blobContainerClient;
        }

        public async Task<BlobInfo> GetBlobAsync(string name)
        {
            var blobClient = _blobContainerClient.GetBlobClient(name);
            var download = await blobClient.DownloadAsync();

            return new BlobInfo(download.Value.Content, download.Value.ContentType);
        }

        public async Task<IEnumerable<string>> ListBlobsAsync()
        {
            var items = new List<string>();

            await foreach (var blobItem in _blobContainerClient.GetBlobsAsync())
            {
                items.Add(blobItem.Name);
            }

            return items;
        }
    }
}
