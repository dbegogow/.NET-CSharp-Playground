using System.Diagnostics;

namespace ReflectionDelegatesDemo;

public class Program
{
    static void Main()
    {
        var homeController = new HomeController();
        var homeControllerType = homeController.GetType();

        var property = homeControllerType.GetProperties()
            .FirstOrDefault(pr => pr.IsDefined(typeof(DataAttribute), true));

        var getMethod = property.GetMethod;

        var stopWatch = Stopwatch.StartNew();

        for (int i = 0; i < 100000; i++)
        {
            var dict = (IDictionary<string, object>)getMethod.Invoke(homeController, Array.Empty<object>());
        }

        Console.WriteLine(stopWatch.Elapsed);

        var deleg = PropertyHelper
            .MakeFastPropertyGetter<IDictionary<string, object>>(property);

        stopWatch = Stopwatch.StartNew();

        for (int i = 0; i < 100000; i++)
        {
            var dict = deleg(homeController);
        }

        Console.WriteLine(stopWatch.Elapsed);
    }
}