using System;
using System.Collections.Generic;
using WeatherSensors.Api.Models;

namespace WeatherSensors.Api.Services
{
    public interface IMeasurementFileNameGenerator
    {
        IDictionary<SensorType, string> Generate(string deviceId, DateTime date, SensorType? sensorType);
    }
}
