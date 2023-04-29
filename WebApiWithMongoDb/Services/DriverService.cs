using WebApiWithMongoDb.Configurations;
using WebApiWithMongoDb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WebApiWithMongoDb.Services;

public class DriverService : IDriverService
{
    private readonly IMongoCollection<Driver> _driverCollection;

    public DriverService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.Database);

        this._driverCollection = mongoDb.GetCollection<Driver>(databaseSettings.Value.CollectionName);
    }

    public async Task<IEnumerable<Driver>> GetAsync()
        => await this._driverCollection.Find(_ => true).ToListAsync();

    public async Task<Driver> GetAsync(string id)
        => await this._driverCollection.Find(d => d.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Driver driver)
        => await this._driverCollection.InsertOneAsync(driver);

    public async Task UpdateAsync(Driver driver)
        => await this._driverCollection.ReplaceOneAsync(d => d.Id == driver.Id, driver);

    public async Task RemoveAsync(string id)
        => await this._driverCollection.DeleteOneAsync(d => d.Id == id);
}
