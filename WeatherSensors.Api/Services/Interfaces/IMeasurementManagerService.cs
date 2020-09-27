using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherSensors.Api.Models;
using WeatherSensors.Api.Responses;

namespace WeatherSensors.Api.Services.Interfaces
{
    public interface IMeasurementManagerService
    {
        Task<IEnumerable<IMeasurementResponse>> GetData(string deviceId, DateTime date, SensorType? sensorType);
    }
}
