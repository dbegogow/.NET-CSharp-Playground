using RegisterServiceWithReflection.Engine;
using RegisterServiceWithReflection.Services;
using RegisterServiceWithReflection.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = new HostBuilder();

builder.ConfigureServices(services =>
{
    services.AddSingleton<Worker>();
    services.AddSingleton<ICatalogService, CatalogService>();
});

var host = builder.Build();

using (host)
{
    var worker = host.Services.GetRequiredService<Worker>();

    await worker.RunAsynnc();
}