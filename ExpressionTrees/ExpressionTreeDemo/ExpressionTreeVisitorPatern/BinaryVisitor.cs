﻿using System;
using System.Linq.Expressions;

namespace ExpressionTreeVisitorPatern
{
    public class BinaryVisitor : Visitor
    {
        private readonly BinaryExpression node;
        public BinaryVisitor(BinaryExpression node) : base(node)
        {
            this.node = node;
        }

        public override void Visit(string prefix)
        {
            Console.WriteLine($"{prefix}This binary expression is a {NodeType} expression");
            var left = CreateFromExpression(node.Left);
            Console.WriteLine($"{prefix}The Left argument is:");
            left.Visit(prefix + "-");
            var right = CreateFromExpression(node.Right);
            Console.WriteLine($"{prefix}The Right argument is:");
            right.Visit(prefix + "-");

            Console.WriteLine($"{prefix}Left Operand: {node.Left} - Right Operand: {node.Right}");
        }
    }
}
