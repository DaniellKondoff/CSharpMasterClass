using ExpressionTreeDemo;
using System;
using System.Linq.Expressions;

namespace ExpressionTreeVisitorPatern
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = 50;
            Expression<Func<Cat, string>> catExpression = cat => cat.SayMew(number);
            Expression<Func<int, int, int>> sum = (x, y) => x + y;
            Expression<Func<int>> constExpression = () => 5;

            var visitor = Visitor.CreateFromExpression(catExpression);
            visitor.Visit(string.Empty);
        }
    }
}
