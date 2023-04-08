using InMemoryCachingWebApi.Data;
using InMemoryCachingWebApi.Models;
using InMemoryCachingWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InMemoryCachingWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private readonly ICacheService _cacheService;
    private readonly ApiDbContext _dbContext;

    public DriversController(
        ICacheService cacheService,
        ApiDbContext dbContext)
    {
        this._cacheService = cacheService;
        this._dbContext = dbContext;
    }

    [HttpGet("drivers")]
    public async Task<IActionResult> Get()
    {
        var cacheDrivers = this._cacheService
            .GetData<IEnumerable<Driver>>("drivers");

        if (cacheDrivers != null
            && cacheDrivers.Count() >= 0)
        {
            return Ok(cacheDrivers);
        }

        var drivers = await this._dbContext
            .Drivers
            .ToListAsync();

        var expiryTime = DateTimeOffset.Now.AddMinutes(2);

        this._cacheService.SetData<IEnumerable<Driver>>("drivers", drivers, expiryTime);

        return Ok(drivers);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Driver driver)
    {
        await this._dbContext.Drivers.AddAsync(driver);
        await this._dbContext.SaveChangesAsync();

        return Ok(driver);
    }
}
