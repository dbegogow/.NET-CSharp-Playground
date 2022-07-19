namespace CalculatePowerOfNumber;

public class Program
{
    public static void Main()
    {
        Console.Write("Enter the number: ");
        var number = int.Parse(Console.ReadLine());

        Console.Write("Enter the exponent: ");
        var exponent = int.Parse(Console.ReadLine());

        var power = CalculatePower(number, exponent);

        Console.WriteLine($"Power is: {power}");
    }

    private static int CalculatePower(int number, int exponent)
    {
        if (exponent < 0)
        {
            throw new InvalidOperationException("Invalid exponent");
        }
        else if (exponent == 1)
        {
            return number;
        }
        else if (exponent == 0)
        {
            return 1;
        }
        else
        {
            return number * CalculatePower(number, exponent - 1);
        }
    }
}