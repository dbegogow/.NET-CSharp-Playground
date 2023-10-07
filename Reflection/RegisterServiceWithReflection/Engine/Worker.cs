namespace RegisterServiceWithReflection.Engine;

using RegisterServiceWithReflection.Services.Interfaces;

public class Worker
{
    private readonly ICatalogService catalogService;

    public Worker(ICatalogService catalogService)
    {
        this.catalogService = catalogService;
    }

    public async Task RunAsynnc()
    {
    }
}
