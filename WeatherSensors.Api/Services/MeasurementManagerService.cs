using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using WeatherSensors.Api.Models;
using WeatherSensors.Api.Responses;

namespace WeatherSensors.Api.Services
{
    public class MeasurementManagerService : IMeasurementManagerService
    {
        private readonly IMeasurementFileNameGenerator _measurementFileNameGenerator;
        private readonly IBlobService _blobService;
        private readonly IBlobDownloaderService _blobDownloaderService;
        private readonly IFileCacheService _fileCacheService;

        public MeasurementManagerService(
            IMeasurementFileNameGenerator measurementFileNameGenerator,
            IBlobService blobService,
            IBlobDownloaderService blobDownloaderService,
            IFileCacheService fileCacheService)
        {
            _measurementFileNameGenerator = measurementFileNameGenerator;
            _blobService = blobService;
            _blobDownloaderService = blobDownloaderService;
            _fileCacheService = fileCacheService;
        }

        public async Task<IEnumerable<IMeasurementResponse>> GetData(string deviceId, DateTime date,
            SensorType? sensorType)
        {
            // TODO - Rewrite this method with ChainOfResponsibility pattern (or pipes)

            var sensorFileNameDict = _measurementFileNameGenerator.Generate(deviceId, date, sensorType);

            foreach (var (sensor, blobFileName) in sensorFileNameDict)
            {
                var fileName = blobFileName.Replace('/', Path.DirectorySeparatorChar);

                // 1. Check for exists file and returns it
                if (_fileCacheService.CheckFileExists(fileName))
                {
                    return _fileCacheService.GetFile(fileName, sensor);
                }

                // 2. If not exists then try to download file
                try
                {
                    var blobInfo = await _blobService.GetBlobAsync(blobFileName);
                    await _blobDownloaderService.Download(fileName, blobInfo);
                }
                catch (RequestFailedException)
                {
                }

                // if (!_fileCacheService.CheckFileExists())

                // TODO - 3. If not exists blob try to find in historical.zip
                // TODO - 4. If not exists blob try to download new historical (if newer) and find in new one
                // TODO - 5. If not exists then throw Exception or handle it in different way
                // TODO - 6. Convert CsvFile to proper response
            }

            return Enumerable.Empty<IMeasurementResponse>();
        }
    }
}
