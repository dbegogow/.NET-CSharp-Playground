namespace BasicDelegates;

public class Person
{
    public string Name { get; set; }

    public void SomePersonMethod(int number)
    {

    }
}

public class Program
{
    public static void Main()
    {
        //MyVoidDelegate voidDelegate = PrintInteger;

        var voidDelegate = new MyVoidDelegate(PrintInteger);

        voidDelegate += SomeMethod;
        voidDelegate += PrintInteger;

        var person = new Person();

        voidDelegate += person.SomePersonMethod;

        //voidDelegate -= SomeMethod;

        //voidDelegate += x => Console.WriteLine("Inline func.");
        //voidDelegate += x => Console.WriteLine("Inline func 2.");
        //voidDelegate += x => Console.WriteLine("Inline func 3.");

        voidDelegate?.Invoke(10);
        //voidDelegate?.DynamicInvoke(10); // When you don't know the type of the delegate

        //voidDelegate(100);

        Console.WriteLine("---------");

        Console.WriteLine(voidDelegate?.Target?.GetType().Name);
    }

    public static void PassSomeDelegate(MyVoidDelegate del)
    {
        del(5);
    }

    public static void PrintInteger(int number)
    {
        Console.WriteLine(number);
    }

    public static void SomeMethod(int myInt)
    {
        Console.WriteLine(myInt + 10);
    }

    public static void SomeMethodWithString(string text)
    {
        Console.WriteLine(text);
    }

    public static string SomeOtherMethod(int x, int y)
        => x + y.ToString();
}