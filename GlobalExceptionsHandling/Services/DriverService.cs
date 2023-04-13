using GlobalExceptionsHandling.Data;
using GlobalExceptionsHandling.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalExceptionsHandling.Services;

public class DriverService : IDriverService
{
    private readonly ApiDbContext _dbContext;

    public DriverService(ApiDbContext dbContext)
        => this._dbContext = dbContext;

    public async Task<Driver> AddDriver(Driver driver)
    {
        var result = await this._dbContext.Drivers
            .AddAsync(driver);

        await this._dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<bool> DeleteDriver(int id)
    {
        var driver = await this.GetDriverById(id);

        this._dbContext.Remove(driver);

        await this._dbContext.SaveChangesAsync();

        return driver != null ? true : false;
    }

    public async Task<Driver> GetDriverById(int id)
        => await this._dbContext.Drivers
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Driver>> GetDrivers()
        => await this._dbContext.Drivers.ToListAsync();

    public async Task<Driver> UpdateDriver(Driver driver)
    {
        var result = this._dbContext.Drivers
            .Update(driver);

        await this._dbContext.SaveChangesAsync();

        return result.Entity;
    }
}
