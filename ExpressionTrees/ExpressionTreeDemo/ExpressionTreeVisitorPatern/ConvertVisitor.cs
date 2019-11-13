using System;
using System.Linq.Expressions;

namespace ExpressionTreeVisitorPatern
{
    public class ConvertVisitor : Visitor
    {
        private readonly UnaryExpression node;
        public ConvertVisitor(UnaryExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}Converting....");
            CreateFromExpression(node.Operand).Visit(prefix + "-");
        }
    }
}
