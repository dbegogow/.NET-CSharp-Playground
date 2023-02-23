using Microsoft.AspNetCore.Mvc;

namespace ApiKeyAuthentication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
        => Ok("Hello World!");
}
