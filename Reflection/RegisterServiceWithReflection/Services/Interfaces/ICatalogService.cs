namespace RegisterServiceWithReflection.Services.Interfaces;

public interface ICatalogService
{
    IEnumerable<string> ListAll();
}
