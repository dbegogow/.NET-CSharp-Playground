using CachingWebApiWithRedis.Data;
using CachingWebApiWithRedis.Models;
using CachingWebApiWithRedis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CachingWebApiWithRedis.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private readonly ILogger<DriversController> _logger;
    private readonly ICacheService _cacheService;
    private readonly AppDbContext _context;

    public DriversController(
        ILogger<DriversController> logger,
        ICacheService cacheService,
        AppDbContext context)
    {
        this._logger = logger;
        this._cacheService = cacheService;
        this._context = context;
    }

    [HttpGet("drivers")]
    public async Task<IActionResult> Get()
    {
        var cacheData = this._cacheService
            .GetData<IEnumerable<Driver>>("drivers");

        if (cacheData != null && cacheData.Count() > 0)
        {
            return Ok(cacheData);
        }

        cacheData = await this._context.Drivers
            .ToListAsync();

        var expiryTime = DateTimeOffset.Now.AddSeconds(30);

        this._cacheService.SetData<IEnumerable<Driver>>("drivers", cacheData, expiryTime);

        return Ok(cacheData);
    }

    [HttpPost("AddDriver")]
    public async Task<IActionResult> Post(Driver value)
    {
        var addedObj = await _context.Drivers
            .AddAsync(new Driver
            {
                Name = value.Name,
                DriveNb = value.DriveNb,
            });

        var expiryTime = DateTimeOffset.Now.AddSeconds(30);

        this._cacheService.SetData<Driver>($"driver{value.Id}", addedObj.Entity, expiryTime);

        await this._context.SaveChangesAsync();

        return Ok(addedObj.Entity);
    }

    [HttpDelete("DeleteDriver")]
    public async Task<IActionResult> Delete(int id)
    {
        var exist = await this._context.Drivers
            .FirstOrDefaultAsync(x => x.Id == id);

        if (exist != null)
        {
            this._context.Remove(exist);
            this._cacheService.RemoveData($"driver{id}");

            await this._context.SaveChangesAsync();

            return NoContent();
        }

        return NotFound();
    }
}
