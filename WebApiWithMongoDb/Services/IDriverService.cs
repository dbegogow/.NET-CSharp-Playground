using WebApiWithMongoDb.Models;

namespace WebApiWithMongoDb.Services;

public interface IDriverService
{
    Task<IEnumerable<Driver>> GetAsync();

    Task<Driver> GetAsync(string id);

    Task CreateAsync(Driver driver);

    Task UpdateAsync(Driver driver);

    Task RemoveAsync(string id);
}
