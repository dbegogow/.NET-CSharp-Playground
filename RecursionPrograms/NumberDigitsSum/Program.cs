namespace NumberDigitsSum;

public class Program
{
    public static void Main()
    {
        Console.Write("Enter the number: ");
        var number = int.Parse(Console.ReadLine());

        var sum = CalculateDigitsSum(number);

        Console.WriteLine($"Number sum of digits: {sum}");
    }

    private static int CalculateDigitsSum(int number)
    {
        if (number == 0)
        {
            return 0;
        }

        return number % 10 + CalculateDigitsSum(number / 10);
    }
}