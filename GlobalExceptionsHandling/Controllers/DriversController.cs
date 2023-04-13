using GlobalExceptionsHandling.Models;
using GlobalExceptionsHandling.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalExceptionsHandling.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private readonly ILogger<DriversController> _logger;

    private readonly IDriverService _driverService;

    public DriversController(
        ILogger<DriversController> logger,
        IDriverService driverService)
    {
        this._logger = logger;
        this._driverService = driverService;
    }

    [HttpGet("DriverList")]
    public async Task<IActionResult> DriverList()
    {
        var drivers = await this._driverService
            .GetDrivers();

        return Ok(drivers);
    }

    [HttpPost("AddDriver")]
    public async Task<IActionResult> AddDriver(Driver driver)
    {
        var result = await this._driverService
            .AddDriver(driver);

        return Ok(result);
    }

    [HttpGet("GetDriverById")]
    public async Task<IActionResult> GetDriverById(int id)
    {
        var driver = await this._driverService
            .GetDriverById(id);

        if (driver == null)
        {
            return NotFound();
        }

        return Ok(driver);
    }
}
