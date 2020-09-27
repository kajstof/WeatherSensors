using System;
using System.Collections.Generic;
using System.Linq;
using WeatherSensors.Api.Models;
using WeatherSensors.Api.Responses;

namespace WeatherSensors.Api.Extensions
{
    public static class SensorTypeExtension
    {
        public static SensorType? ConvertToSensorType(this string text)
        {
            var sensorKeyValuePairs = typeof(SensorType).GetEnumValues().Cast<SensorType>()
                .Select(x => new KeyValuePair<string, SensorType>(x.ToString().ToLower(), x));

            var sensorsDict = new Dictionary<string, SensorType>(sensorKeyValuePairs);

            if (sensorsDict.Keys.Any(x => x.Equals(text)))
            {
                return sensorsDict[text];
            }

            return null;
        }

        // TODO - Move this functionality to Attribute - ResponseTypeAttribute
        public static Type GetResponseType(this SensorType sensorType)
        {
            switch (sensorType)
            {
                case SensorType.Humidity:
                    return typeof(MeasurementHumidityResponse);
                case SensorType.Rainfall:
                    return typeof(MeasurementRainfallResponse);
                case SensorType.Temperature:
                    return typeof(MeasurementTemperatureResponse);
                default:
                    throw new ArgumentOutOfRangeException(nameof(sensorType), sensorType, null);
            }
        }
    }
}
