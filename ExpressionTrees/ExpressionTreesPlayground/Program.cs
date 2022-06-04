using System.Linq.Expressions;

namespace ExpressionTreesPlayground;

public class Program
{
    public static void Main()
    {
        var list = new List<int>()
            .Where(n => n % 2 == 0);

        var myClass = new MyClass();

        Func<MyClass, string> func = c => c.MyMethod(42, "My demo code");
        Func<MyClass, bool> propFunc = c => c.MyProperty;

        Expression<Func<MyClass, string>> expr = c => c.MyMethod(42, "My demo code");
        Expression<Func<MyClass, bool>> propExpr = c => c.MyProperty;

        ParseExpression(expr);
        ParseExpression(propExpr);
    }

    private static void ParseExpression(Expression expression)
    {
        if (expression.NodeType == ExpressionType.Lambda)
        {
            var lambdaExpression = (LambdaExpression)expression;

            Console.Write("Lambda ");
            Console.WriteLine(lambdaExpression.Parameters[0].Name);

            ParseExpression(lambdaExpression.Body);
        }
        else if (expression.NodeType == ExpressionType.Call)
        {
            var methodCallExpression = (MethodCallExpression)expression;

            Console.Write("Method ");

            Console.WriteLine(methodCallExpression.Method.Name);

            for (int i = 0; i < methodCallExpression.Arguments.Count; i++)
            {
                ParseExpression(methodCallExpression.Arguments[i]);
            }
        }
        else if (expression.NodeType == ExpressionType.MemberAccess)
        {
            var memberExpression = (MemberExpression)expression;

            Console.Write("Property ");

            Console.WriteLine(memberExpression.Member.Name);
        }
        else if (expression.NodeType == ExpressionType.Constant)
        {
            var constantExpression = (ConstantExpression)expression;

            Console.Write("Constant ");

            Console.WriteLine(constantExpression.Value);
        }
    }
}