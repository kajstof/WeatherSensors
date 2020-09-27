using System;

namespace WeatherSensors.Api.Responses
{
    public abstract class MeasurementResponseBase<T> : IMeasurementResponse
    {
        public DateTime DateTime { get; set; }
        public T Value { get; set; }
    }
}
