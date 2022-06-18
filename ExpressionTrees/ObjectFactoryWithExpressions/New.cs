using System.Linq.Expressions;

namespace ObjectFactoryWithExpressions;

public static class New<T>
    where T : new()
{
    public static Func<T> Instance =
        Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
}
