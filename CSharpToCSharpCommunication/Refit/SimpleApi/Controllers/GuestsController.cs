using Microsoft.AspNetCore.Mvc;

using SimpleApi.Models;

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
    public IEnumerable<GuestModel> Get()
        => guests;
}
