using RegisterServiceWithReflection.Engine;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RegisterServiceWithReflection.Infrastructure.Extensions;

var builder = new HostBuilder();

builder.ConfigureServices(services => services.AddConventionalServices());

var host = builder.Build();

using (host)
{
    var worker = host.Services.GetRequiredService<Worker>();

    worker.Run();
}

// Library for register services automatically by convention - https://github.com/khellang/Scrutor