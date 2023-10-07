namespace RegisterServiceWithReflection.Services.Interfaces;

using RegisterServiceWithReflection.Infrastructure.ServiceInterfaces;

public interface IRentService : ISingletonService
{
    string ChooseCar(string idNumber);
}
