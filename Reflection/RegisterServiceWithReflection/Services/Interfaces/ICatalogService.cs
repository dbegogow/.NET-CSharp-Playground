namespace RegisterServiceWithReflection.Services.Interfaces;

using RegisterServiceWithReflection.Infrastructure.Services;

public interface ICatalogService : IService
{
    IEnumerable<string> ListAll();
}
