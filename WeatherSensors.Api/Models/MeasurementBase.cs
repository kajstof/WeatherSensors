using System;

namespace WeatherSensors.Api.Models
{
    public abstract class MeasurementBase<T>
    {
        public DateTime DateTime { get; set; }
        public T Value { get; set; }
    }
}
