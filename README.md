# WeatherSensors [![WeatherSensors](https://circleci.com/gh/kajstof/WeatherSensors.svg?style=svg)](https://circleci.com/gh/kajstof/WeatherSensors)

WeatherSensors IoT project 

## WIP

## WeatherSensors.Api

Example endpoints:
- api/v1/devices/dockan/data/2019-01-10/humidity
- api/v1/devices/dockan/data/2019-01-10/rainfall
- api/v1/devices/dockan/data/2019-01-10/temperature
- api/v1/devices/dockan/data/2019-01-10
- getdata?deviceId=dockan&date=2019-01-10&sensor=humidity
- getdata?deviceId=dockan&date=2019-01-10&sensor=rainfall
- getdata?deviceId=dockan&date=2019-01-10&sensor=temperature
- getdata?deviceId=dockan&date=2019-01-10

## Unit Tests

At this moment there is only one class covered by Unit Tests: [MeasurementFileNameGeneratorTests.cs](https://github.com/kajstof/WeatherSensors/blob/master/WeatherSensors.Tests/MeasurementFileNameGeneratorTests.cs)

### MeasurementManagerService.cs

* (34, 16) // TODO - Rewrite this method with ChainOfResponsibility pattern (or pipes)
* (60, 20) // TODO - 3. If not exists blob try to find in historical.zip
* (61, 20) // TODO - 4. If not exists blob try to download new historical (if newer) and find in new one
* (62, 20) // TODO - 5. If not exists then throw Exception or handle it in different way
* (63, 20) // TODO - 6. Convert CsvFile to proper response

### SensorTypeExtension.cs

* (26, 12) // TODO - Move this functionality to Attribute - ResponseTypeAttribute
