using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WeatherSensors.Api.Models;
using WeatherSensors.Api.Services.Interfaces;

namespace WeatherSensors.Api.Services
{
    public class BlobDownloaderService : IBlobDownloaderService
    {
        private readonly string _currentDomainBaseDirectory = AppDomain.CurrentDomain.BaseDirectory ?? string.Empty;
        private readonly string _blobCacheDirectory;

        public BlobDownloaderService(IConfiguration configuration)
        {
            _blobCacheDirectory = configuration.GetValue<string>("FileCacheOutputDirectory");
        }

        public async Task Download(string fileName, BlobInfo blobInfo)
        {
            var fullFileName = Path.Combine(_currentDomainBaseDirectory, _blobCacheDirectory, fileName);
            var directory = Path.GetDirectoryName(fullFileName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await using FileStream file = File.OpenWrite(fullFileName);
            await blobInfo.Content.CopyToAsync(file);
            file.Close();
        }
    }
}
