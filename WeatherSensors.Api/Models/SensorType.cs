using System.ComponentModel;
using WeatherSensors.Api.Attributes;
using WeatherSensors.Api.Responses;
using WeatherSensors.Api.TypeConverters;

namespace WeatherSensors.Api.Models
{
    [TypeConverter(typeof(SensorTypeConverter))]
    public enum SensorType
    {
        [ResponseType(typeof(MeasurementHumidityResponse))]
        Humidity,

        [ResponseType(typeof(MeasurementRainfallResponse))]
        Rainfall,

        [ResponseType(typeof(MeasurementTemperatureResponse))]
        Temperature
    }
}
