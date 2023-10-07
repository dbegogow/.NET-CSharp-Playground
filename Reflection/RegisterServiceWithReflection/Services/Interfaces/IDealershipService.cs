namespace RegisterServiceWithReflection.Services.Interfaces;

using RegisterServiceWithReflection.Infrastructure.ServiceInterfaces;

public interface IDealershipService : IScopedService
{
    bool Register(
        string model,
        decimal price,
        string firstName,
        string lastName);
}
