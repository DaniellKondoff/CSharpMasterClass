using System;
using System.Linq.Expressions;

namespace ExpressionTreeVisitorPatern
{
    public class LambdaVisitor : Visitor
    {
        private readonly LambdaExpression node;
        public LambdaVisitor(LambdaExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}Extracting lambda....");
            Console.WriteLine($"{prefix}This expression is a {NodeType} expression type");
            Console.WriteLine($"{prefix}The name of the lambda is {((node.Name == null) ? "<null>" : node.Name)}");
            Console.WriteLine($"{prefix}The return type is {node.ReturnType.ToString()}");
            Console.WriteLine($"{prefix}The expression has {node.Parameters.Count} argument(s). They are:");

            foreach (var parameterExpression in node.Parameters)
            {
                var parametarVisitor = CreateFromExpression(parameterExpression);
                parametarVisitor.Visit(prefix + "-");
            }

            Console.WriteLine($"{prefix}The expression body is:");
            var bodyVisitor = CreateFromExpression(node.Body);
            bodyVisitor.Visit(prefix + "-");
        }
    }
}
