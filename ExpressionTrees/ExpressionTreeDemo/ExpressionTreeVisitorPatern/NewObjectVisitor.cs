using System;
using System.Linq.Expressions;

namespace ExpressionTreeVisitorPatern
{
    public class NewObjectVisitor : Visitor
    {
        private readonly NewExpression node;
        public NewObjectVisitor(NewExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}Extracting Object Creation....");
            Console.WriteLine($"{prefix}Constructor: {node.Constructor.DeclaringType.Name}");

            foreach (var argument in node.Arguments)
            {
                CreateFromExpression(argument).Visit(prefix + "-");
            }
        }
    }
}
