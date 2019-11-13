using System;
using System.Linq.Expressions;

namespace ExpressionTreeVisitorPatern
{
    public abstract class Visitor
    {
        private readonly Expression node;

        protected Visitor(Expression node)
        {
            this.node = node;
        }

        public abstract void Visit(string prefix);

        public ExpressionType NodeType => this.node.NodeType;

        public static Visitor CreateFromExpression(Expression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Lambda:
                    return new LambdaVisitor((LambdaExpression)node);
                case ExpressionType.Parameter:
                    return new ParametarVisitor((ParameterExpression)node);
                case ExpressionType.Add:
                    return new BinaryVisitor((BinaryExpression)node);
                case ExpressionType.Constant:
                    return new ConstantVisitor((ConstantExpression)node);
                case ExpressionType.Call:
                    return new MethodVisitor((MethodCallExpression)node);
                case ExpressionType.MemberAccess:
                    return new MemberAccessVisitor((MemberExpression)node);
                case ExpressionType.New:
                    return new NewObjectVisitor((NewExpression)node);
                case ExpressionType.MemberInit:
                    return new MemberInitVisitor((MemberInitExpression)node);
                case ExpressionType.Convert:
                    return new ConvertVisitor((UnaryExpression)node);
                default:
                    Console.Error.WriteLine($"Node not processed yet: {node.NodeType}");
                    return default;
            }
        }
    }
}
