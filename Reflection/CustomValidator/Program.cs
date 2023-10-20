namespace CustomValidator;

public class Program
{
    public static void Main()
    {
        var cat = new Cat
        {
            Name = "John",
            Age = 2,
            Color = "Black"
        };

        var secondCat = new Cat();

        var validator = new ObjectValidator();

        var result = validator.Validate(cat);

        PrintErrors(result);

        result = validator.Validate(secondCat);

        PrintErrors(result);
    }

    private static void PrintErrors(ValidationResult result)
    {
        Console.WriteLine(result.IsValid ? "Valid" : "Invalid");

        foreach (var error in result.Errors)
        {
            Console.WriteLine(error.Key);


            foreach (var errorMessage in error.Value)
            {
                Console.WriteLine($"--- {errorMessage}");
            }
        }

        Console.WriteLine(new string('-', 50));
    }
}