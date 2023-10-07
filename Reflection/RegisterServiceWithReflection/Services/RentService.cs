using RegisterServiceWithReflection.Services.Interfaces;

namespace RegisterServiceWithReflection.Services;

public class RentService : IRentService
{
    private readonly IReadOnlyDictionary<string, string> cars = new Dictionary<string, string>
    {
        { "IH5TYU", "Mercesed E63s - 2000$ per day" },
        { "Y8OP9S", "Audi R8 - 2500$ per day" }
    };

    public string ChooseCar(string idNumber)
        => this.cars[idNumber];
}
