namespace RegisterServiceWithReflection.Infrastructure.Extensions;

using RegisterServiceWithReflection.Infrastructure.Services;
using RegisterServiceWithReflection.Infrastructure.ServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConventionalServices(this IServiceCollection services)
    {
        var serviceInterfaceType = typeof(IService);
        var singletonInterfaceType = typeof(ISingletonService);
        var scopedServiceInterface = typeof(IScopedService);

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

        return services;
    }
}
