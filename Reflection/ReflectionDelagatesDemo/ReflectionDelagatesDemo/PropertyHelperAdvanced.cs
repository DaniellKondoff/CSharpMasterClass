using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace ReflectionDelagatesDemo
{
    public class PropertyHelperAdvanced
    {
        private static IDictionary<string, Delegate> cache = new ConcurrentDictionary<string, Delegate>();

        private static readonly MethodInfo CallInnerDelegateMethod =
            typeof(PropertyHelperAdvanced).GetMethod(nameof(CallInnerDelegate), BindingFlags.NonPublic | BindingFlags.Static);

        public static Func<object, TResult> MakeFastPropertyGetter<TResult>(PropertyInfo property)
        {
            if (cache.ContainsKey(property.Name))
            {
                return (Func<object, TResult>)cache[property.Name];
            }

            var getMethod = property.GetMethod;
            var declaringClass = property.DeclaringType;
            var typeOfResult = typeof(TResult);
            
            //Func<ControllerType, TResult>
            var getMethodDelegateType = typeof(Func<,>).MakeGenericType(declaringClass, typeof(TResult));

            // c => c.Data
            var getMethodDelegate = getMethod.CreateDelegate(getMethodDelegateType);

            //CallInnerDelegate<ControllerType, TResult>
            var callInnerGenericMethodWithTypes =  CallInnerDelegateMethod.MakeGenericMethod(declaringClass, typeOfResult);

            // Func<object, TResult>
            var result = (Func<object, TResult>)callInnerGenericMethodWithTypes.Invoke(null, new[] { getMethodDelegate });

            cache.Add(property.Name, result);

            return result;
        }

        private static Func<object, TResult> CallInnerDelegate<TClass, TResult>(Func<TClass, TResult> deleg)
        {
            return instance => deleg((TClass)instance);
        }
    }
}
