namespace WebApiWithMongoDb.Configurations;

public class DatabaseSettings
{
    public string? ConnectionString { get; set; }

    public string? Database { get; set; }

    public string? CollectionName { get; set; }
}
