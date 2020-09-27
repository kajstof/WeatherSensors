using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using WeatherSensors.Api.Models;
using WeatherSensors.Api.Services.Interfaces;

namespace WeatherSensors.Api.Services
{
    public class MeasurementFileNameGenerator : IMeasurementFileNameGenerator
    {
        private readonly string _blobNameFormat;
        private readonly string _blobNameHistoricalZip;

        public MeasurementFileNameGenerator(IConfiguration configuration)
        {
            _blobNameFormat = configuration.GetValue<string>("BlobNameFormat");
            _blobNameHistoricalZip = configuration.GetValue<string>("BlobNameHistoricalZip");
        }

        public IDictionary<SensorType, string> Generate(string deviceId, DateTime date, SensorType? sensorType)
        {
            return Generate(deviceId, sensorType, FormatFile);

            string FormatFile(string sensor) => string.Format(_blobNameFormat, deviceId.ToLower(), sensor, date);
        }

        public IDictionary<SensorType, string> GenerateHistorical(string deviceId, SensorType? sensorType)
        {
            return Generate(deviceId, sensorType, FormatHistoricalFile);

            string FormatHistoricalFile(string sensor) =>
                string.Format(_blobNameHistoricalZip, deviceId.ToLower(), sensor);
        }

        private IDictionary<SensorType, string> Generate(string deviceId, SensorType? sensorType,
            Func<string, string> formatFunc)
        {
            var sensorDict = InitSensorDictionary(sensorType);
            IDictionary<SensorType, string> results = new Dictionary<SensorType, string>();

            foreach (var (key, value) in sensorDict)
            {
                results.Add(key, formatFunc(value));
            }

            return results;
        }

        private static Dictionary<SensorType, string> InitSensorDictionary(SensorType? sensorType)
        {
            var sensorDict = new Dictionary<SensorType, string>();
            if (sensorType.HasValue)
            {
                sensorDict.Add(sensorType.Value, sensorType.ToString()?.ToLower());
            }
            else
            {
                foreach (var sensor in typeof(SensorType).GetEnumValues())
                {
                    sensorDict.Add((SensorType) sensor, sensor.ToString()?.ToLower());
                }
            }

            return sensorDict;
        }
    }
}
