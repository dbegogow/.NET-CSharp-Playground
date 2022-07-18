namespace ConvertIntegerToBinary;

public class Program
{
    public static void Main()
    {
        Console.Write("Enter the number: ");
        var number = int.Parse(Console.ReadLine());

        BinaryConversion(number);
    }

    private static int BinaryConversion(int number)
    {
        if (number == 0)
        {
            return 0;
        }

        var bin = (number % 2) + 10 * BinaryConversion(number / 2);
        Console.Write(bin);

        return 0;
    }
}