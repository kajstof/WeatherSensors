using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherSensors.Api.Models;
using WeatherSensors.Api.Services;
using WeatherSensors.Api.Services.Interfaces;

namespace WeatherSensors.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class DevicesController : ControllerBase
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly IMeasurementManagerService _measurementManagerService;

        public DevicesController(ILogger<DevicesController> logger,
            IMeasurementManagerService measurementManagerService)
        {
            _logger = logger;
            _measurementManagerService = measurementManagerService;
        }

        [HttpGet("getdata")]
        [HttpGet("api/v1/devices/{deviceId}/data/{date}")]
        [HttpGet("api/v1/devices/{deviceId}/data/{date}/{sensorType}")]
        public async Task<IActionResult> DeviceDataGet(string deviceId, DateTime date, SensorType? sensorType)
        {
            _logger.LogInformation($"Getting data: {deviceId} | {date:yyy-MM-dd} | {sensorType.ToString()}");
            var measurementData = await _measurementManagerService.GetData(deviceId, date, sensorType);
            return Ok(measurementData);
        }
    }
}
