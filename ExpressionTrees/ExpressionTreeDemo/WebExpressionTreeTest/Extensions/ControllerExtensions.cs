using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebExpressionTreeTest.Extensions
{
    public static class ControllerExtensions
    {
        
        public static IActionResult RedirectToAction<TController>(this Controller controller, Expression<Action<TController>> redirectExpression)
        {
            if(redirectExpression.Body is MethodCallExpression methodCall)
            {
                var actionName = methodCall.Method.Name;
                var controllerName = typeof(TController).Name.Replace(nameof(Controller), string.Empty);

                var routeValueDictionary = new RouteValueDictionary();

                var parametors = methodCall
                    .Method
                    .GetParameters()
                    .Select(p => p.Name)
                    .ToArray();

                var values = methodCall
                    .Arguments
                    .Select(arg =>
                    {
                        var constant = (ConstantExpression)arg;
                        return constant.Value;
                    })
                    .ToArray();

                for (int i = 0; i < parametors.Length; i++)
                {
                    routeValueDictionary.Add(parametors[i], values[i]);
                }

                return controller.RedirectToAction(actionName, controllerName, routeValueDictionary);
            }
            else
            {
                throw new InvalidOperationException("Expression is not valid");
            }
        }

        public static IActionResult RedirectToAction<TController>(this TController controller, Expression<Action<TController>> redirectExpression)
        {
            throw new NotImplementedException();
        }

        public static IActionResult RedirectTo<TController>(this Controller controller, Expression<Action<TController>> redirectExpression)
        {
            if(redirectExpression.Body.NodeType != ExpressionType.Call)
            {
                throw new InvalidOperationException($"The provided expression is not a valid method call: {redirectExpression.Body.NodeType}");
            }

            var methodCallExpression = (MethodCallExpression)redirectExpression.Body;

            var actionName = GetActionName(methodCallExpression);
            var controllerName = typeof(TController).Name.Replace(nameof(Controller), string.Empty);

            var routeValuesDict = ExtractRouteValues(methodCallExpression);

            return controller.RedirectToAction(actionName, controllerName, routeValuesDict);
        }

        public static IActionResult RedirectTo<TController>(this TController controller, Expression<Action<TController>> redirectExpression)
        {
            return null;
        }

        private static RouteValueDictionary ExtractRouteValues(MethodCallExpression exoression)
        {
            //[id, query]
            var parameters = exoression.Method
                .GetParameters()
                .Select(pr => pr.Name)
                .ToArray();

            var values = exoression.Arguments
                .Select(arg =>
                {
                    if (arg.NodeType == ExpressionType.Constant)
                    {
                        var constantExpression = (ConstantExpression)arg;
                        return constantExpression.Value;
                    }

                    //() => (object)arg -> Func<object>
                    var convertExpression = Expression.Convert(arg, typeof(object));
                    var funcExpression = Expression.Lambda<Func<object>>(convertExpression);
                    return funcExpression.Compile().Invoke();
                })
                .ToArray();

            var routeValueDict = new RouteValueDictionary();

            for (int i = 0; i < parameters.Length; i++)
            {
                routeValueDict.Add(parameters[i], values[i]);
            }

            return routeValueDict;
        }

        private static string GetActionName(MethodCallExpression expression)
        {
            var cacheKey = $"{expression.Method.Name}_{expression.Object.Type.Name}";

            var methodName = expression.Method.Name;

            var actionName = expression.Method
                .GetCustomAttributes(true)
                .OfType<ActionNameAttribute>()
                .FirstOrDefault()
                ?.Name;

            return actionName ?? methodName;
        }
    }
}
