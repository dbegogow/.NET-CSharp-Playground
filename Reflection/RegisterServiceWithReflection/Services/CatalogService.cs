namespace RegisterServiceWithReflection.Services;

using RegisterServiceWithReflection.Services.Interfaces;

public class CatalogService : ICatalogService
{
    private readonly IReadOnlyCollection<string> cars = new List<string>
    {
        "Mercedes",
        "Audit",
        "BMW",
        "Porche",
        "Lamborghini",
        "Ferrari"
    };

    public IEnumerable<string> ListAll()
        => cars;
}
