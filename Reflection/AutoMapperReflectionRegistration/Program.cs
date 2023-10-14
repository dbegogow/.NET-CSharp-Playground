using AutoMapperReflectionRegistration.Engine;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = new HostBuilder();

builder.ConfigureServices(services =>
{
    services.AddSingleton<Worker>();

    services.AddAutoMapper(typeof(Program));
});

var host = builder.Build();

using (host)
{
    var worker = host.Services.GetRequiredService<Worker>();

    worker.Run();
}
