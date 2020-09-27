using System.Collections.Generic;
using WeatherSensors.Api.Models;
using WeatherSensors.Api.Responses;

namespace WeatherSensors.Api.Services.Interfaces
{
    public interface IFileCacheService
    {
        bool CheckFileExists(string fileName);
        IEnumerable<IMeasurementResponse> GetFile(string fileName, SensorType sensorType);
    }
}
