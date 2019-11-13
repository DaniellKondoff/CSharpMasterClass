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
            Expression<Func<object>> constConvertExpression = () => (object)5;
            Expression<Func<Cat, string>> property = cat => cat.Owner.FullName;
            Expression<Func<string, string, Cat>> catCreationExp = (CatName, CatOwnerName) => new Cat(CatName);
            Expression<Func<string, string, Cat>> catOwnerCreationExp = (CatName, OwnerName) => new Cat(CatName)
            {
                Owner = new Owner
                {
                    FullName = OwnerName
                }
            };

            var visitor = Visitor.CreateFromExpression(catOwnerCreationExp);
            visitor.Visit(string.Empty);
        }
    }
}
