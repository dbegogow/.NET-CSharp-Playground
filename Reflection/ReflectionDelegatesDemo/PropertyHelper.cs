using System.Reflection;
using System.Collections.Concurrent;

namespace ReflectionDelegatesDemo;

public class PropertyHelper
{
    private static readonly ConcurrentDictionary<string, Delegate> cache = new ConcurrentDictionary<string, Delegate>();

    private static readonly MethodInfo CallInnerDelegateMethod =
        typeof(PropertyHelper).GetMethod(nameof(CallInnerDelegate), BindingFlags.NonPublic | BindingFlags.Static);

    public static Func<object, TResult> MakeFastPropertyGetter<TResult>(PropertyInfo property)
        => (Func<object, TResult>)cache.GetOrAdd(property.Name, key =>
            {
                var getMethod = property.GetMethod;
                var declaringClass = property.DeclaringType;
                var typeOfResult = typeof(TResult);

                var getMethodDelegateType = typeof(Func<,>).MakeGenericType(declaringClass, typeOfResult);

                var getMethodDelegate = getMethod.CreateDelegate(getMethodDelegateType);

                var callInnerGenericMethodWithTypes = CallInnerDelegateMethod
                    .MakeGenericMethod(declaringClass, typeOfResult);

                var result = (Delegate)callInnerGenericMethodWithTypes.Invoke(null, new[] { getMethodDelegate });

                return result;
            });

    private static Func<object, TResult> CallInnerDelegate<TClass, TResult>(
        Func<TClass, TResult> deleg)
        => instance => deleg((TClass)instance);
}
