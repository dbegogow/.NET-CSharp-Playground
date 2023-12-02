using CoolMultipleAwaits;

var service = new WeatherServices();

Console.WriteLine(TimeProvider.System.GetLocalNow());

var londonTask = service.GetWeather("London");
var parisTask = service.GetWeather("Paris");

var weathers = await (londonTask, parisTask);

Console.WriteLine(TimeProvider.System.GetLocalNow());