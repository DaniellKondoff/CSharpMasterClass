using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;

namespace ObjectCreationWithCtors
{
    public static class ObjectFactory
    {
        public static T CreateInstance<T>(this Type type) where T : new()
        {
            return New<T>.Instance();
        }

        public static object CreateInstance<TArg>(this Type type, TArg argument)
        {
            return CreateInstance<TArg, TypeToIgnore>(type, argument, null);
            //var constructor = type.GetConstructor(new Type[] { typeof(TArg) });
            //var exprParam = Expression.Parameter(typeof(TArg), "argument");

            //var expNew = Expression.New(constructor, exprParam);

            //var lambda = Expression.Lambda<Func<TArg, object>>(expNew, exprParam);

            //var func = lambda.Compile();

            //return func(argument);
        }

        public static object CreateInstance<TArg1, TArg2>(this Type type, TArg1 argument1, TArg2 argument2)
        {
            return CreateInstance<TArg1, TArg2, TypeToIgnore>(type, argument1, argument2, null);
        }

       
        public static object CreateInstance<TArg1, TArg2, TArg3>(this Type type, TArg1 argument1, TArg2 argument2, TArg3 argument3)
        {
            return ObjectFactoryCreator<TArg1, TArg2, TArg3>.CreateInstance(type, argument1, argument2, argument3);
        }

        public static T CreateInstanceByT<T,TArg>(this Type type, TArg argument)
        {
            var constructor = type.GetConstructor(new Type[] { typeof(TArg) });

            var exprParam = Expression.Parameter(typeof(TArg), "argument");

            var expNew = Expression.New(constructor, exprParam);

            var lambda = Expression.Lambda<Func<TArg, T>>(expNew, exprParam);

            var func = lambda.Compile();

            return func(argument);
        }

        private class TypeToIgnore
        {

        }


        // (TArg1 arg1, TArg2 arg2, TArg3 arg3) => new Type(arg1, arg2, arg3)
        private static class ObjectFactoryCreator<TArg1, TArg2, TArg3>
        {
            private static ConcurrentDictionary<Type, Func<TArg1, TArg2, TArg3, object>> objectFactoryCache
                = new ConcurrentDictionary<Type, Func<TArg1, TArg2, TArg3, object>>();

            //public Func<Type, TArg1, TArg2, TArg3, object>
            public static object CreateInstance(Type type, TArg1 argument1, TArg2 argument2, TArg3 argument3)
            {
                var objectFactoryFunc = objectFactoryCache.GetOrAdd(type, _ =>
                {
                    var argumentTypes = new[]
                    {
                    typeof(TArg1),
                    typeof(TArg2),
                    typeof(TArg3)

                    };

                    var constructorArgumentType = argumentTypes
                        .Where(t => t != typeof(TypeToIgnore))
                        .ToArray();


                    var constructor = type.GetConstructor(constructorArgumentType);

                    if (constructor == null)
                    {
                        throw new InvalidOperationException($"{type.Name} does not contain a constructor for the provided arguments");
                    }


                    // (TArg1 arg1, TArg2 arg2, TArg3 arg3)
                    var expressionParameters = argumentTypes
                        .Select((t, i) => Expression.Parameter(t, $"arg{i}"))
                        .ToArray();


                    var expressionCOnstructorParameters = expressionParameters
                        .Take(constructorArgumentType.Length)
                        .ToArray();

                    // new Type(arg1, arg2, arg3);
                    var newExpression = Expression.New(constructor, expressionCOnstructorParameters);

                    var lambdaExpression = Expression.Lambda<Func<TArg1, TArg2, TArg3, object>>(newExpression, expressionParameters);

                    return lambdaExpression.Compile();
                });

                return objectFactoryFunc(argument1, argument2, argument3);
            }
        }
    }
}
