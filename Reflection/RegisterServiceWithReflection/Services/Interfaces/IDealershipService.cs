namespace RegisterServiceWithReflection.Services.Interfaces;

public interface IDealershipService
{
    bool Register(
        string model,
        decimal price,
        string firstName,
        string lastName);
}
