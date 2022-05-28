using SimpleApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace SimpleApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestsController : ControllerBase
{
    private static List<GuestModel> guests = new()
    {
        new GuestModel { Id = 1, FirstName = "Dzhulio", LastName = "Begogov" },
        new GuestModel { Id = 2, FirstName = "Ivana", LastName = "Tagareva" },
        new GuestModel { Id = 1, FirstName = "Ivaylo", LastName = "Nikolov" }
    };

    [HttpGet]
    public IEnumerable<GuestModel> GetAllGuests()
        => guests;

    [HttpGet("{id}")]
    public GuestModel GetGuests(int id)
        => guests.FirstOrDefault(g => g.Id == id);

    [HttpPost]
    public void Post([FromBody] GuestModel value)
        => guests.Add(value);

    [HttpPut("{id}")]
    public void Post(int id, [FromBody] GuestModel value)
    {
        var guest = guests
            .FirstOrDefault(g => g.Id == id);

        if (guest == null)
        {
            return;
        }

        guest.Id = value.Id;
        guest.FirstName = value.FirstName;
        guest.LastName = value.LastName;
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        var guest = guests
            .FirstOrDefault(g => g.Id == id);

        if (guest == null)
        {
            return;
        }

        guests.Remove(guest);
    }
}
