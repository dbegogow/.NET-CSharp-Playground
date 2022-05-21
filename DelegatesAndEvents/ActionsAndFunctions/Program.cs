namespace ActionsAndFunctions;

public class Person
{
    public string Name { get; set; }

    public void SayHello()
    {
        Console.WriteLine($"Hello, I'm {this.Name}");
    }
}

public class Program
{
    public static void Main()
    {
        Action<int, string, bool> action = SomeMethod;

        action += (x, y, z) => Console.WriteLine(x);

        action(10, "text", true);

        Func<string, bool, int> func = SomeMethod;

        Func<int, int, int> sumFunc = (x, y) => x + y;

        Console.WriteLine(sumFunc(5, 10));

        Action<Person> personAction = person => person.SayHello();

        var person = new Person
        {
            Name = "Dzhulio"
        };

        personAction(person);

        Func<Person, string> personFunc = person => person.Name;

        Console.WriteLine(personFunc(person));

        Calculate(person => person.Name);
        Calculate(person => person.Name + " this time");
    }

    public static void Calculate(Func<Person, string> func)
    {
        var person = new Person
        {
            Name = "Dzhulio1"
        };

        var result = func(person);

        Console.WriteLine(result);
    }

    public static int SomeMethod(string text, bool someBool)
    {
        Console.WriteLine("Calling." + text);
        return 42;
    }

    public static void SomeMethod(int number, string text, bool someBool)
    {
        Console.WriteLine("Test");
    }
}