using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionTreeVisitorPatern
{
    public class MemberAccessVisitor : Visitor
    {
        private readonly MemberExpression node;
        public MemberAccessVisitor(MemberExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}Extracting property Member....");

            if (node.Member is PropertyInfo property)
            {
                Console.WriteLine($"{prefix}Property - {property.Name} - {property.PropertyType.Name}");
            }

            if (node.Member is FieldInfo field)
            {
                Console.WriteLine($"{prefix}Field - {field.Name} - {field.FieldType.Name}");

                var classIntance = (ConstantExpression)node.Expression;
                var variable = field.GetValue(classIntance.Value);
                Console.WriteLine($"{prefix}Variable: {variable}");
            }

            CreateFromExpression(node.Expression).Visit(prefix);
        }
    }
}
