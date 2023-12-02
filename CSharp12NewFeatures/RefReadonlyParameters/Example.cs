namespace RefReadonlyParameters;

public class Example
{
    public void Test(ref readonly int age)
    {
        Console.WriteLine($"The age is: {age}");

        // age++;
    }
}
