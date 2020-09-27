# WeatherSensors
WeatherSensors IoT project

## WIP

### WeatherSensors.Api

MeasurementManagerService.cs
(34, 16) // TODO - Rewrite this method with ChainOfResponsibility pattern (or pipes)
(60, 20) // TODO - 3. If not exists blob try to find in historical.zip
(61, 20) // TODO - 4. If not exists blob try to download new historical (if newer) and find in new one
(62, 20) // TODO - 5. If not exists then throw Exception or handle it in different way
(63, 20) // TODO - 6. Convert CsvFile to proper response
SensorTypeExtension.cs
(26, 12) // TODO - Move this functionality to Attribute - ResponseTypeAttribute
