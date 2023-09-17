using System.Reflection;

namespace DynamicReflectionDemo;

public class Program
{
    public static void Main()
    {
        var assembly = Assembly.Load(new AssemblyName("CoolLibrary"));

        var type = assembly.GetType("CoolLibrary.SomeCoolClass");

        dynamic someCoolClass = new ExposedObject(type);

        string result = someCoolClass.CoolMethod(42, "Something cool");

        Console.WriteLine(result);

        someCoolClass.Name = "My cool name";
    }
}