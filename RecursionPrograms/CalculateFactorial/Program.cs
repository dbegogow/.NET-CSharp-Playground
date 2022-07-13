namespace CalculateFactorial;

public class Program
{
    public static void Main()
    {
        Console.Write("Enter the number: ");
        var number = int.Parse(Console.ReadLine());

        var fact = CalculateFactorial(number);

        Console.WriteLine($"Factorial from given number: {fact}");
    }

    private static int CalculateFactorial(int number)
    {
        if (number == 1)
        {
            return 1;
        }

        return number * CalculateFactorial(number - 1);
    }
}