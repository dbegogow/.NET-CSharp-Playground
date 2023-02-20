using System.Reflection;
using System.Collections.Concurrent;

namespace ReflectionDelegatesDemo;

public class PropertyHelper<TClass>
{
    private static readonly IDictionary<string, Delegate> cache = new ConcurrentDictionary<string, Delegate>();

    public static Func<TClass, TResult> MakeFastPropertyGetter<TResult>(PropertyInfo property)
    {
        if (cache.ContainsKey(property.Name))
        {
            return (Func<TClass, TResult>)cache[property.Name];
        }

        var getMethod = property.GetMethod;

        var deleg = (Func<TClass, TResult>)getMethod.CreateDelegate(typeof(Func<TClass, TResult>));

        cache.Add(property.Name, deleg);

        return deleg;
    }
}
