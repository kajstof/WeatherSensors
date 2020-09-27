using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using WeatherSensors.Api.Extensions;
using WeatherSensors.Api.Models;
using WeatherSensors.Api.Responses;
using WeatherSensors.Api.Services.Interfaces;

namespace WeatherSensors.Api.Services
{
    public class FileCacheService : IFileCacheService
    {
        private readonly string _currentDomainBaseDirectory = AppDomain.CurrentDomain.BaseDirectory ?? string.Empty;
        private readonly string _blobCacheDirectory;

        public FileCacheService(IConfiguration configuration)
        {
            _blobCacheDirectory = configuration.GetValue<string>("FileCacheOutputDirectory");
        }

        public bool CheckFileExists(string fileName)
        {
            var fullFileName = Path.Combine(_currentDomainBaseDirectory, _blobCacheDirectory, fileName);
            return File.Exists(fullFileName);
        }

        public IEnumerable<IMeasurementResponse> GetFile(string fileName, SensorType sensorType)
        {
            var fullFileName = Path.Combine(_currentDomainBaseDirectory, _blobCacheDirectory, fileName);

            using var reader = new StreamReader(fullFileName);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            //
            return (IEnumerable<IMeasurementResponse>) csv.GetRecords(sensorType.GetResponseType());
        }
    }
}
