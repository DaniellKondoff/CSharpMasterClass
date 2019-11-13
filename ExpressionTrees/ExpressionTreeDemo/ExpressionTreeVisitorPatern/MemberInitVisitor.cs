using System;
using System.Linq.Expressions;

namespace ExpressionTreeVisitorPatern
{
    public class MemberInitVisitor : Visitor
    {
        private readonly MemberInitExpression node;
        public MemberInitVisitor(MemberInitExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}Extracting Object Creation with Members....");
            CreateFromExpression(node.NewExpression).Visit(prefix + "-");

            foreach (var memberBinding in node.Bindings)
            {
                Console.WriteLine($"{prefix}Extracting Member....");
                Console.WriteLine($"{prefix}Member: {memberBinding.Member.Name}");

                var memberAssignment = (MemberAssignment)memberBinding;

                CreateFromExpression(memberAssignment.Expression).Visit(prefix + "-");
            }
        }
    }
}
