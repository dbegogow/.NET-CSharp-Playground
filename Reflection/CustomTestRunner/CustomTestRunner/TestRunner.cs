using System.Reflection;
using CustomTestRunner.Framework;

namespace CustomTestRunner;

public class TestRunner
{
    public static void ExecuteTests(params Type[] types)
    {
        var assemblies = types
            .Select(t => t.Assembly)
            .ToArray();

        var tests = FindTests(assemblies);

        foreach (var test in tests)
        {
            var testComponents = test.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            var givenComponents = GetTestComponents(testComponents, typeof(Given));
            var becauseComponents = GetTestComponents(testComponents, typeof(Because));
            var itComponents = GetTestComponents(testComponents, typeof(It));

            var testInstance = Activator.CreateInstance(test);

            RunGivenComponents(givenComponents, testInstance);
            RunBecauseComponents(becauseComponents, testInstance);
            RunItComponents(itComponents, testInstance);
        }
    }

    private static List<Type> FindTests(params Assembly[] assemblies)
        => assemblies
            .SelectMany(a => a.ExportedTypes)
            .Where(t => t.IsDefined(typeof(SubjectAttribute), false))
            .ToList();

    private static List<FieldInfo> GetTestComponents(
        IEnumerable<FieldInfo> fields,
        Type typeOfComponent)
    {
        var components = fields
                .Where(f => f.FieldType == typeOfComponent)
                .ToList();

        if (!components.Any())
        {
            throw new InvalidOperationException($"Test does not contain {typeOfComponent.Name} specification.");
        }

        return components;
    }

    private static void RunGivenComponents(
        IEnumerable<FieldInfo> testComponents,
        object test)
    {
        var values = GetComponents<Given>(testComponents, test);

        foreach (var value in values)
        {
            value.Invoke();
        }
    }

    private static void RunBecauseComponents(
        IEnumerable<FieldInfo> testComponents,
        object test)
    {
        var values = GetComponents<Because>(testComponents, test);

        foreach (var value in values)
        {
            value.Invoke();
        }
    }

    private static void RunItComponents(
        IEnumerable<FieldInfo> testComponents,
        object test)
    {
        var values = GetComponents<It>(testComponents, test);

        foreach (var value in values)
        {
            value.Invoke();
        }
    }

    private static IEnumerable<TComponent> GetComponents<TComponent>(
        IEnumerable<FieldInfo> testComponents,
        object test)
        => testComponents
            .Select(tc => tc.GetValue(test))
            .Cast<TComponent>();
}
