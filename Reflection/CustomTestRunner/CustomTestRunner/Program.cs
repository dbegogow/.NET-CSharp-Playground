using CustomTestRunner.Tests;

namespace CustomTestRunner;

public class Program
{
    public static void Main()
        => TestRunner.ExecuteTests(typeof(Car));
}