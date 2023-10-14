using AutoMapperReflectionRegistration.Engine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder();

//builder.ConfigureServices(services => services.AddConventionalServices());

var host = builder.Build();

using (host)
{
    var worker = host.Services.GetRequiredService<Worker>();

    worker.Run();
}
