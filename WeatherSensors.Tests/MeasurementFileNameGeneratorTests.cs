using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Moq;
using WeatherSensors.Api.Models;
using WeatherSensors.Api.Services;
using WeatherSensors.Api.Services.Interfaces;
using Xunit;

namespace WeatherSensors.Tests
{
    public class MeasurementFileNameGeneratorTests
    {
        private readonly string _blobNameFormat = "{0}/{1}/{2:yyyy-MM-dd}.csv";
        private readonly string _blobNameHistoricalZip = "{0}/{1}/historical.zip";

        private readonly IMeasurementFileNameGenerator _sut;

        public MeasurementFileNameGeneratorTests()
        {
            var configuration = Mock.Of<IConfiguration>(x =>
                x.GetSection("BlobNameFormat") == Mock.Of<IConfigurationSection>(y => y.Value == _blobNameFormat) &&
                x.GetSection("BlobNameHistoricalZip") ==
                Mock.Of<IConfigurationSection>(y => y.Value == _blobNameHistoricalZip));

            _sut = new MeasurementFileNameGenerator(configuration);
        }

        [Theory]
        [InlineData(SensorType.Humidity)]
        [InlineData(SensorType.Rainfall)]
        [InlineData(SensorType.Temperature)]
        public void Generate_whenValidSensorTypeIsProvided_shouldReturnOneFileNameEntry(SensorType sensorType)
        {
            // Arrange via ctor
            // Act
            var generatedNames = _sut.Generate("MyDevice", new DateTime(2000, 10, 15), sensorType);

            // Assert
            Assert.Single(generatedNames);
            Assert.Contains(generatedNames, x => x.Key.Equals(sensorType));
            Assert.Contains(generatedNames,
                x => x.Value.Equals($"mydevice/{sensorType.ToString().ToLower()}/2000-10-15.csv"));
        }

        [Fact]
        public void Generate_whenNoSensorTypeIsProvided_shouldReturnAllPossibleFileNameEntries()
        {
            // Arrange via ctor
            // Act
            var generatedNames = _sut.Generate("MyDevice", new DateTime(2000, 10, 15), null);

            // Assert
            var expectedKeys = typeof(SensorType).GetEnumValues().Cast<SensorType>();
            var expectedValues =
                typeof(SensorType).GetEnumNames().Select(x => $"mydevice/{x.ToLower()}/2000-10-15.csv");

            Assert.Equal(typeof(SensorType).GetEnumNames().Length, generatedNames.Count);
            Assert.Contains(generatedNames, x => expectedKeys.Any(y => y.Equals(x.Key)));
            Assert.Contains(generatedNames, x => expectedValues.Any(y => y.Equals(x.Value)));
        }

        [Theory]
        [InlineData(SensorType.Humidity)]
        [InlineData(SensorType.Rainfall)]
        [InlineData(SensorType.Temperature)]
        public void GenerateHistorical_whenValidSensorTypeIsProvided_shouldReturnOneFileNameEntry(SensorType sensorType)
        {
            // Arrange in ctor
            // Act
            var generatedNames = _sut.GenerateHistorical("MyDevice", sensorType);

            // Assert
            Assert.Single(generatedNames);
            Assert.Contains(generatedNames, x => x.Key.Equals(sensorType));
            Assert.Contains(generatedNames,
                x => x.Value.Equals($"mydevice/{sensorType.ToString().ToLower()}/historical.zip"));
        }

        [Fact]
        public void GenerateHistorical_whenNoSensorTypeIsProvided_shouldReturnAllPossibleFileNameEntries()
        {
            // Arrange via ctor
            // Act
            var generatedNames = _sut.GenerateHistorical("MyDevice", null);

            // Assert
            var expectedKeys = typeof(SensorType).GetEnumValues().Cast<SensorType>();
            var expectedValues =
                typeof(SensorType).GetEnumNames().Select(x => $"mydevice/{x.ToLower()}/historical.zip");

            Assert.Equal(typeof(SensorType).GetEnumNames().Length, generatedNames.Count);
            Assert.Contains(generatedNames, x => expectedKeys.Any(y => y.Equals(x.Key)));
            Assert.Contains(generatedNames, x => expectedValues.Any(y => y.Equals(x.Value)));
        }
    }
}
