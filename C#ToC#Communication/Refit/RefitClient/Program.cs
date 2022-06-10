using RefitCommon;
using Refit;

namespace RefitClient;

public class Program
{
    public static async Task Main()
    {
        var service = RestService.For<IPersonService>("https://localhost:7101");

        var newPerson = new Person { Id = 4, Name = "Stefan", Age = 25 };

        await service.AddPerson(newPerson);

        var people = await service.GetAll();

        foreach (var person in people)
        {
            Console.Write($"Id: {person.Id}, ");
            Console.Write($"Name: {person.Name}, ");
            Console.WriteLine($"Age: {person.Age}");
        }
    }
}