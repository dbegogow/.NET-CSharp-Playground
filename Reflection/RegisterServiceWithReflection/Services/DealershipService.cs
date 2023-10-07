namespace RegisterServiceWithReflection.Services;

using RegisterServiceWithReflection.Services.Interfaces;

public class DealershipService : IDealershipService
{
    public bool Register(
        string model,
        decimal price,
        string firstName,
        string lastName)
    {
        Console.WriteLine(
            $"Successfully register your car: Model - {model}, Price: {price}, First name - {firstName}, Last name - {lastName}");

        return true;
    }
}
