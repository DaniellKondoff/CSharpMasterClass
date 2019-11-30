using System;
using System.Linq.Expressions;

namespace ObjectCreationWithCtors
{
    public class New<T> where T : new()
    {
        public static Func<T> Instance => Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
    }
}
