using System;
using System.Linq.Expressions;

namespace ExpressionTreeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var someInt = 50;
            Expression<Func<Cat, string>> constant = cat => cat.SayMew(50);

            ParseExpression(constant, string.Empty);
        }

        private static void ParseExpression(Expression expression, string level)
        {
            level += "-";

            if (expression.NodeType == ExpressionType.Lambda)
            {
                Console.WriteLine($"{level}Extracting lambda....");
                var lambdaExpression = (LambdaExpression)expression;
                var body = lambdaExpression.Body;

                Console.WriteLine($"{level}Extracting body....");
                ParseExpression(body, level);

                Console.WriteLine($"{level}Extracting parameters....");

                foreach (var prams in lambdaExpression.Parameters)
                {
                    ParseExpression(prams, level);
                }
            }
            else if (expression.NodeType == ExpressionType.Constant)
            {
                Console.WriteLine($"{level}Extracting constant....");
                var constExpression = (ConstantExpression)expression;

                var value = constExpression.Value;

                Console.WriteLine($"{level}Constant - {value}");
            }
            else if (expression.NodeType == ExpressionType.Convert)
            {
                Console.WriteLine($"{level}Converting....");
                var unaryExpression = (UnaryExpression)expression;

                ParseExpression(unaryExpression.Operand, level);
            }
            else if (expression.NodeType == ExpressionType.Call)
            {
                Console.WriteLine($"{level}Extracting Method....");
                var methodExpression = (MethodCallExpression)expression;

                Console.WriteLine($"{level}Method Name: {methodExpression.Method.Name}");
               
                if(methodExpression.Object == null)
                {
                    Console.WriteLine($"{level}Method is static");
                }
                else
                {
                    ParseExpression(methodExpression.Object, level);
                }

                Console.WriteLine($"{level}Extracting Method Arguments....");
                foreach (var argumnet in methodExpression.Arguments)
                {
                    Console.WriteLine($"{level}Extracting Argument");
                    ParseExpression(argumnet, level);
                }
            }
            else if(expression.NodeType == ExpressionType.Parameter)
            {
                Console.WriteLine($"{level}Extracting parameter....");
                var paramExpression = (ParameterExpression)expression;


                Console.WriteLine($"{level}Parametar - {paramExpression.Name} - {paramExpression.Type.Name}");
            }
        }
    }
}
