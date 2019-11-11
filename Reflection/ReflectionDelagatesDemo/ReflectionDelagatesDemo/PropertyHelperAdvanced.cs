using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ReflectionDelagatesDemo
{
    public class PropertyHelperAdvanced
    {
        private static readonly MethodInfo CallInnerDelegateMethod =
            typeof(PropertyHelperAdvanced).GetMethod(nameof(CallInnerDelegate), BindingFlags.NonPublic | BindingFlags.Static);

        public static Func<object, TResult> MakeFastPropertyGetter<TResult>(PropertyInfo property)
        {
            var getMethod = property.GetMethod;
            var declaringClass = property.DeclaringType;
            var typeOfResult = typeof(TResult);
            
            //Func<ControllerType, TResult>
            var getMethodDelegateType = typeof(Func<,>).MakeGenericType(declaringClass, typeof(TResult));

            var getMethodDelegate = getMethod.CreateDelegate(getMethodDelegateType);

            CallInnerDelegateMethod.MakeGenericMethod(declaringClass, typeOfResult);

            return (Func<object, TResult>)getMethodDelegate;
        }

        private static Func<object, TResult> CallInnerDelegate<TClass, TResult>(Func<TClass, TResult> deleg)
        {
            return instance => deleg((TClass)instance);
        }
    }
}
