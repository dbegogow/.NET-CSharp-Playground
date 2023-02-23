using ApiKeyAuthentication.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ApiKeyAuthentication.Controllers;

[ApiController]
[Route("api/[controller]")]
//[ServiceFilter(typeof(ApiKeyAuthFilter))]
public class TestController : ControllerBase
{
    [HttpGet("hello")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    //[ApiKeyAuthFilter]
    public IActionResult Get()
        => Ok("Hello World!");

    [HttpGet("hello-again")]
    public IActionResult GetAgain()
        => Ok("Hello World again!");
}
