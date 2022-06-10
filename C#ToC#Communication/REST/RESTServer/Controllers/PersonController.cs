using RESTCommon;
using Microsoft.AspNetCore.Mvc;

namespace RESTServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private static readonly List<Person> People = new()
    {
        new () {Id = 1, Name = "Dzhulio", Age = 19 },
        new () {Id = 2, Name = "Ivana", Age = 20 },
        new () {Id = 3, Name = "Ivaylo", Age = 32 }
    };

    [HttpGet]
    public IActionResult Get() => Ok(People);

    [HttpPost]
    public IActionResult Post(Person person)
    {
        if (string.IsNullOrWhiteSpace(person.Name)
            || person.Age <= 0)
        {
            return BadRequest();
        }

        People.Add(person);

        return Ok();
    }
}
