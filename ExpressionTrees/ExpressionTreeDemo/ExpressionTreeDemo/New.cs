using System;
using System.Linq.Expressions;

namespace ExpressionTreeDemo
{
    public class New<T>
        where T : class
    {
        public static Func<T> Instance = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
    }
}
