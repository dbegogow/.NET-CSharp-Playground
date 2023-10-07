namespace RegisterServiceWithReflection.Engine;

using RegisterServiceWithReflection.Services.Interfaces;

public class Worker
{
    private readonly ICatalogService catalogService;
    private readonly IDealershipService dealershipService;
    private readonly IRentService rentService;

    public Worker(
        ICatalogService catalogService,
        IDealershipService dealershipService,
        IRentService rentService)
    {
        this.catalogService = catalogService;
        this.dealershipService = dealershipService;
        this.rentService = rentService;

    }

    public void Run()
    {
        var carsCatalog = this.catalogService
            .ListAll()
            .ToList();

        Console.WriteLine($"Cars catalog: {string.Join(", ", carsCatalog)}");

        Console.WriteLine(new string('-', 100));

        Console.WriteLine("Register car:");

        Console.Write($"Model: ");
        var model = Console.ReadLine();

        Console.Write($"Price: ");
        var price = decimal.Parse(Console.ReadLine());

        Console.Write($"First name: ");
        var firstName = Console.ReadLine();

        Console.Write($"Last name: ");
        var lastName = Console.ReadLine();

        var carRegistration = this.dealershipService
            .Register(model, price, firstName, lastName);

        Console.WriteLine(carRegistration);

        Console.WriteLine(new string('-', 100));

        Console.WriteLine("Rent a car");

        Console.Write($"Car id number: ");
        var idNumber = Console.ReadLine();

        var choosedCar = this.rentService
            .ChooseCar(idNumber);

        Console.WriteLine($"Choosed car: {choosedCar}");
    }
}
