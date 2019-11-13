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
    }
}
