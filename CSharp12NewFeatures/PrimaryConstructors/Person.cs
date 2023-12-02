namespace PrimaryConstructors;
public class Person(string name)
{
    public void PrintName()
        => Console.WriteLine(name);
}

record User(string name);
