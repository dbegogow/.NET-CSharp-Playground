using Microsoft.AspNetCore.Mvc;

namespace SimpleApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestsController : ControllerBase
{
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }
}
