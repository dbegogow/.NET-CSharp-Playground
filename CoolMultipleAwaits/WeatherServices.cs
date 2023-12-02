namespace CoolMultipleAwaits;

public class WeatherServices
{
    private static readonly string[] Summaries = {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot"
    };

    public async Task<IEnumerable<WeatherForecast>> GetWeather(string city)
    {
        await Task.Delay(2000);

        var weather = Enumerable.Range(1, 5).Select(i => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(i),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        return weather;
    }
}
