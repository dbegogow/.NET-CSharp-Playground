using System.Diagnostics;

namespace PropertyHelperWithExpressions;

public class Program
{
    public static void Main()
    {
        // return RedirectToAction("Index", new { id = 5, query = "Text" });
        // Dictionary { ["id"] = 5, ["query"] = "Text" }

        var obj = new { id = 5, query = "Text" };

        var dict = new Dictionary<string, object>();

        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < 1_000_000; i++)
        {
            obj
                .GetType()
                .GetProperties()
                .Select(pr => new
                {
                    pr.Name,
                    Value = pr.GetValue(obj)
                })
                .ToList()
                .ForEach(pr => dict[pr.Name] = pr.Value ?? string.Empty);
        }

        Console.WriteLine($"{stopwatch.Elapsed} - Reflection Property Getters");
        Console.WriteLine(dict.Count);

        dict = new Dictionary<string, object>();

        stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < 1_000_000; i++)
        {
            PropertyHelper
                .GetProperties(obj.GetType())
                .Select(pr => new
                {
                    pr.Name,
                    Value = pr.Getter(obj)
                })
                .ToList()
                .ForEach(pr => dict[pr.Name] = pr.Value);
        }

        Console.WriteLine($"{stopwatch.Elapsed} - Expression Tree Getters");
        Console.WriteLine(dict.Count);
    }
}