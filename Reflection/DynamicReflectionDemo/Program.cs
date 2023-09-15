using System.Reflection;

namespace DynamicReflectionDemo;

public class Program
{
    public static void Main()
    {
        var assembly = Assembly.Load(new AssemblyName("CoolLibrary"));

        var type = assembly.GetType("CoolLibrary.SomeCoolClass");

        var method = type.GetMethod("CoolMethod", BindingFlags.NonPublic | BindingFlags.Static);

        var result = (string)method.Invoke(null, new object[] { 42, "some text" });

        Console.WriteLine(result);
    }
}