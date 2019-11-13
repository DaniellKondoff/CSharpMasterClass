using System;
using System.Linq.Expressions;

namespace ExpressionTreeVisitorPatern
{
    public class MethodVisitor : Visitor
    {
        private readonly MethodCallExpression node;
        public MethodVisitor(MethodCallExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}Method Name: {node.Method.Name}");

            if (node.Object == null)
            {
                Console.WriteLine($"{prefix}Method is static");
            }
            else
            {
                CreateFromExpression(node.Object).Visit(prefix);
            }

            Console.WriteLine($"{prefix}Extracting Method Arguments....");
            foreach (var argumnet in node.Arguments)
            {
                Console.WriteLine($"{prefix}Extracting Argument");
                CreateFromExpression(argumnet).Visit(prefix);
            }
        }
    }
}
