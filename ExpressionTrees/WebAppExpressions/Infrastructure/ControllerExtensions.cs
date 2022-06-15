using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace WebAppExpressions.Infrastructure
{
    public static class ControllerExtensions
    {
        public static IActionResult RedirectTo<TController>(
            this Controller controller,
            Expression<Action<TController>> redirectExpression)
        {
            if (redirectExpression.Body.NodeType != ExpressionType.Call)
            {
                throw new InvalidOperationException($"The provided expression is not a valid method call: {redirectExpression.Body}");
            }

            var methodCallExpression = (MethodCallExpression)redirectExpression.Body;

            var actionName = methodCallExpression.Method.Name;
            var controllerName = typeof(TController).Name.Replace(nameof(Controller), string.Empty);

            return controller.RedirectToAction(actionName, controllerName);
        }
    }
}
