namespace RegisterServiceWithReflection.Infrastructure.Extensions;

using RegisterServiceWithReflection.Engine;
using RegisterServiceWithReflection.Infrastructure.Services;
using RegisterServiceWithReflection.Infrastructure.ServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConventionalServices(this IServiceCollection services)
    {
        services.AddSingleton<Worker>();

        var serviceInterfaceType = typeof(IService);
        var singletonInterfaceType = typeof(ISingletonService);
        var scopedServiceInterfaceType = typeof(IScopedService);

        var types = serviceInterfaceType
            .Assembly
            .GetExportedTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t => new
            {
                Service = t.GetInterface($"I{t.Name}"),
                Implementation = t
            })
            .Where(t => t.Service != null);

        foreach (var type in types)
        {
            if (serviceInterfaceType.IsAssignableFrom(type.Service))
            {
                services.AddTransient(type.Service, type.Implementation);
            }
            else if (singletonInterfaceType.IsAssignableFrom(type.Service))
            {
                services.AddSingleton(type.Service, type.Implementation);
            }
            else if (scopedServiceInterfaceType.IsAssignableFrom(type.Service))
            {
                services.AddScoped(type.Service, type.Implementation);
            }
        }

        return services;
    }
}
