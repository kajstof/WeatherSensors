using System;

namespace WeatherSensors.Api.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    sealed class ResponseTypeAttribute : Attribute
    {
        public Type ResponseType { get; }

        public ResponseTypeAttribute(Type type)
        {
            this.ResponseType = type;
        }
    }
}
