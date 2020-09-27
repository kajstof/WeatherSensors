using System;
using System.Collections.Generic;
using WeatherSensors.Api.Models;

namespace WeatherSensors.Api.Services.Interfaces
{
    public interface IMeasurementFileNameGenerator
    {
        IDictionary<SensorType, string> Generate(string deviceId, DateTime date, SensorType? sensorType);
        IDictionary<SensorType, string> GenerateHistorical(string deviceId, SensorType? sensorType);
    }
}
