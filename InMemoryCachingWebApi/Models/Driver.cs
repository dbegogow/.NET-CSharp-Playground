namespace InMemoryCachingWebApi.Models;

public class Driver
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int RacingNumber { get; set; }

    public string Team { get; set; } = string.Empty;
}
