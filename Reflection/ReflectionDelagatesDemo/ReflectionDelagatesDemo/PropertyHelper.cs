using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ReflectionDelagatesDemo
{
    public class PropertyHelper<TClass>
    {
        public static Func<TClass, TResult> MakeFastPropertyGetter<TResult>(PropertyInfo property)
        {
            var getMethod = property.GetMethod;

            return (Func<TClass, TResult>)getMethod.CreateDelegate(typeof(Func<TClass, TResult>));
        }
    }
}
