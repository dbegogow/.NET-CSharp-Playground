using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace ObjectFactoryWithExpressions;

public static class ObjectFactory
{
    public static T CreateInstance<T>(this Type type)
        where T : new()
        => New<T>.Instance();

    public static object CreateInstance<TArg>(this Type type, TArg argument)
        => CreateInstance<TArg, TypeToIgnore>(type, argument, null);

    public static object CreateInstance<TArg1, TArg2>(this Type type, TArg1 argument1, TArg2 argument2)
        => CreateInstance<TArg1, TArg2, TypeToIgnore>(type, argument1, argument2, null);

    public static object CreateInstance<TArg1, TArg2, TArg3>(
        this Type type,
        TArg1 argument1,
        TArg2 argument2,
        TArg3 argument3)
        => ObjectFactoryCreator<TArg1, TArg2, TArg3>
            .CreateInstance(type, argument1, argument2, argument3);

    private class TypeToIgnore
    {
    }

    private static class ObjectFactoryCreator<TArg1, TArg2, TArg3>
    {
        private static Type TypeToIgnore = typeof(TypeToIgnore);

        private static ConcurrentDictionary<Type, Func<TArg1, TArg2, TArg3, object>> objectFactoryCache = new();

        public static object CreateInstance(
            Type type,
            TArg1 argument1,
            TArg2 argument2,
            TArg3 argument3)
        {
            var objectFactoryFunc = objectFactoryCache.GetOrAdd(type, _ =>
            {
                var argumentTypes = new[]
                {
                    typeof(TArg1),
                    typeof(TArg2),
                    typeof(TArg3)
                };

                var constructorArgumentTypes = argumentTypes
                    .Where(t => t != TypeToIgnore)
                    .ToArray();

                var constructor = type.GetConstructor(constructorArgumentTypes);

                if (constructor == null)
                {
                    throw new InvalidOperationException($"{type.Name} does not contain a constructor for the provided argument types: {string.Join(", ", constructorArgumentTypes.Select(t => t.Name))}.");
                }

                var expressionParameters = argumentTypes
                    .Select((t, i) => Expression.Parameter(t, $"arg{i}"))
                    .ToArray();

                var expressionConstructorParameters = expressionParameters
                    .Take(constructorArgumentTypes.Length)
                    .ToArray();

                var newExpression = Expression.New(constructor, expressionConstructorParameters);

                var lambdaExpression = Expression
                    .Lambda<Func<TArg1, TArg2, TArg3, object>>(newExpression, expressionParameters);

                return lambdaExpression.Compile();
            });

            return objectFactoryFunc(argument1, argument2, argument3);
        }
    }
}
