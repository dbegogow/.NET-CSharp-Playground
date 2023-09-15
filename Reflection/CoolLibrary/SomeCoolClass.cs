namespace CoolLibrary;

internal static class SomeCoolClass
{
    internal static string CoolMethod(int number, string text)
    {
        Console.WriteLine(number);
        Console.WriteLine(text);

        return number + text;
    }
}