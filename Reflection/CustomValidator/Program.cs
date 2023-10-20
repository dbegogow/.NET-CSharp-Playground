namespace CustomValidator;

public class Program
{
    public static void Main()
    {
        var cat = new Cat
        {
            Color = "Yellow"
        };

        var validator = new ObjectValidator();

        var result = validator.Validate(cat);

        Console.WriteLine(result.IsValid ? "Valid" : "Invalid");

        foreach (var error in result.Errors)
        {
            Console.WriteLine(error.Key);


            foreach (var errorMessage in error.Value)
            {
                Console.WriteLine($"--- {errorMessage}");
            }
        }
    }
}