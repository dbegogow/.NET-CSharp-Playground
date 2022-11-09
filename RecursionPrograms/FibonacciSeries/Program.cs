namespace FibonacciSeries;

internal class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the length of the Fibonacci Series: ");
        var length = int.Parse(Console.ReadLine());

        for (int i = 0; i < length; i++)
        {
            Console.Write(FibonacciSeries(i));
        }
    }

    static int FibonacciSeries(int n)
    {
        if (n == 0)
        {
            return 0;
        }
        else if (n == 1)
        {
            return 1;
        }

        return FibonacciSeries(n - 1) + FibonacciSeries(n - 2);
    }
}