using System;
using System.Linq.Expressions;

namespace ExpressionTreeVisitorPatern
{
    public class ParametarVisitor : Visitor
    {
        private readonly ParameterExpression node;

        public ParametarVisitor(ParameterExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}This is an {NodeType} expression type");
            Console.WriteLine($"{prefix}Parametar - {node.Name} - {node.Type.Name}");
        }
    }
}
