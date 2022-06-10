using System.Text;
using System.Text.Json;
using RESTCommon;

namespace RESTClient;

public class Program
{
    public static async Task Main()
    {
        using var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7068")
        };

        const string url = "/api/person";

        var newPerson = new Person
        {
            Id = 4,
            Name = "Kiril",
            Age = 5
        };

        var jsonBody = JsonSerializer.Serialize(newPerson);

        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        using var postResponse = await httpClient.PostAsync(url, content);

        if (!postResponse.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Bad person");
        }

        using var getResponse = await httpClient.GetAsync(url);

        var jsonResult = await getResponse.Content.ReadAsStringAsync();

        var people = JsonSerializer.Deserialize<IEnumerable<Person>>(jsonResult, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        foreach (var person in people)
        {
            Console.WriteLine($"My name is {person.Name}. I'm {person.Age} years old.");
        }
    }
}
