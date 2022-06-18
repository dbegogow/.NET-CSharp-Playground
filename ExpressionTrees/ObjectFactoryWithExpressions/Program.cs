using System.Diagnostics;

namespace ObjectFactoryWithExpressions
{
    public class Program
    {
        public static void Main()
        {
            var list = new List<Cat>();

            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 100; i++)
            {
                var cat = Activator.CreateInstance<Cat>();

                list.Add(cat);
            }

            Console.WriteLine($"{stopwatch.Elapsed} - Activator - No parameters in constructor");
            Console.WriteLine(list.Count);

            list = new List<Cat>();
            New<Cat>.Instance();
            stopwatch = Stopwatch.StartNew();

            //var catType = typeof(Cat);

            //catType.CreateInstance<Cat>();

            for (int i = 0; i < 100; i++)
            {
                var cat = New<Cat>.Instance();

                list.Add(cat);
            }

            Console.WriteLine($"{stopwatch.Elapsed} - Expression Trees - No parameters in constructor");
            Console.WriteLine(list.Count);
        }
    }
}
