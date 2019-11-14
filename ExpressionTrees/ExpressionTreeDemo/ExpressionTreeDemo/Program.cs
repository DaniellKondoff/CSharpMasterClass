using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ExpressionTreeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var someInt = 50;
            Expression<Func<Cat, string>> catExpression = cat => cat.SayMew(someInt);
            Expression<Func<int, int, int>> sum = (x, y) => x + y;
            Expression<Func<Cat, string>> property = cat => cat.Owner.FullName;
            Expression<Func<string, string, Cat>> catCreationExp = (CatName, OwnerName) => new Cat(CatName)
            {
                Owner = new Owner
                {
                    FullName = OwnerName
                }
            };

            Html.EditorFor<Cat>(c => c.Name);

            //var func = catExpression.Compile();
            //Console.WriteLine(func(new Cat()));

            //ParseExpression(property, string.Empty);

            var typeCat = typeof(Cat);
            //Generating Expresion Trees
            // () => 42

            //42
            var constant = Expression.Constant(42);
            //()=>
            var lambda = Expression.Lambda<Func<int>>(constant);
            var func = lambda.Compile();

            //() => new Cat();
            var catExpr = Expression.New(typeCat);
            var catLambda = Expression.Lambda<Func<Cat>>(catExpr);
            var catConstruction = catLambda.Compile();
            var catt = catConstruction();


            //cat => cat.sayMew(42);
            //42
            var constant42 = Expression.Constant(42);
            //cat
            var catExprParam = Expression.Parameter(typeCat, "cat");
            //Method cat.sayMew(42);
            var methodInfo = typeCat.GetMethod(nameof(Cat.SayMew));
            var call = Expression.Call(catExprParam, methodInfo, constant);

            var lambdaCat = Expression.Lambda<Func<Cat, string>>(call, catExprParam);
            //var funcc = lambdaCat.Compile();
            //Console.WriteLine(funcc(new Cat()));

            //(cat, number) => cat.sayMew(number);
            var catExprParam1 = Expression.Parameter(typeCat, "cat");
            var numberParametar2 = Expression.Parameter(typeof(int), "number");
            //Method cat.sayMew(number);
            var methodInfo2 = typeCat.GetMethod(nameof(Cat.SayMew));
            var call2 = Expression.Call(catExprParam1, methodInfo, numberParametar2);

            var lambdaCat2 = Expression.Lambda<Func<Cat, int, string>>(call2, catExprParam1, numberParametar2);
            //var func2 = lambdaCat2.Compile();
            //Console.WriteLine(func2(new Cat(), 100));

            //MVC
            //RedirectToAction("Index", new {id = 5, query = "Test"})
            //id = 5, query = "Test"
            var obj = new { id = 5, query = "Test" };
            var dict = new Dictionary<string, object>();

            obj.GetType()
                .GetProperties()
                .Select(pr => new
                {
                    Name = pr.Name,
                    Value = pr.GetValue(obj)
                })
                .ToList()
                .ForEach(pr =>
                {
                    dict[pr.Name] = pr.Value;
                });

            PropertyHelper.Get(obj)
                 .Select(pr => new
                 {
                     Name = pr.Name,
                     Value = pr.Getter(obj)
                 })
                .ToList()
                .ForEach(pr =>
                {
                    dict[pr.Name] = pr.Value;
                });

            dict.Count();

            var cat = new Cat("SomeCatname");
            dynamic someClass = new ExposedObject(cat);
            Console.WriteLine(someClass.SomeProperty);
        }

        private static void ParseExpression(Expression expression, string level)
        {
            level += "-";

            if (expression.NodeType == ExpressionType.Lambda)
            {
                Console.WriteLine($"{level}Extracting lambda....");
                var lambdaExpression = (LambdaExpression)expression;
               
                Console.WriteLine($"{level}Extracting parameters....");

                foreach (var prams in lambdaExpression.Parameters)
                {
                    ParseExpression(prams, level);
                }

                var body = lambdaExpression.Body;
                Console.WriteLine($"{level}Extracting body....");
                ParseExpression(body, level);
            }
            else if (expression.NodeType == ExpressionType.Constant)
            {
                Console.WriteLine($"{level}Extracting constant....");
                var constExpression = (ConstantExpression)expression;

                var value = constExpression.Value;

                Console.WriteLine($"{level}Constant - {value}");
            }
            else if (expression.NodeType == ExpressionType.Convert)
            {
                Console.WriteLine($"{level}Converting....");
                var unaryExpression = (UnaryExpression)expression;

                ParseExpression(unaryExpression.Operand, level);
            }
            else if (expression.NodeType == ExpressionType.Call)
            {
                Console.WriteLine($"{level}Extracting Method....");
                var methodExpression = (MethodCallExpression)expression;

                Console.WriteLine($"{level}Method Name: {methodExpression.Method.Name}");
               
                if(methodExpression.Object == null)
                {
                    Console.WriteLine($"{level}Method is static");
                }
                else
                {
                    ParseExpression(methodExpression.Object, level);
                }

                Console.WriteLine($"{level}Extracting Method Arguments....");
                foreach (var argumnet in methodExpression.Arguments)
                {
                    Console.WriteLine($"{level}Extracting Argument");
                    ParseExpression(argumnet, level);
                }
            }
            else if(expression.NodeType == ExpressionType.Parameter)
            {
                Console.WriteLine($"{level}Extracting parameter....");
                var paramExpression = (ParameterExpression)expression;

                Console.WriteLine($"{level}Parametar - {paramExpression.Name} - {paramExpression.Type.Name}");
            }
            else if(expression.NodeType == ExpressionType.MemberAccess)
            {
                Console.WriteLine($"{level}Extracting property Member....");
                var propertyExpression = (MemberExpression)expression;
                
                if(propertyExpression.Member is PropertyInfo property)
                {
                    Console.WriteLine($"{level}Property - {property.Name} - {property.PropertyType.Name}");
                }

                if(propertyExpression.Member is FieldInfo field)
                {
                    Console.WriteLine($"{level}Field - {field.Name} - {field.FieldType.Name}");

                    var classIntance = (ConstantExpression)propertyExpression.Expression;
                    var variable = field.GetValue(classIntance.Value);
                    Console.WriteLine($"{level}Variable: {variable}");
                }

                ParseExpression(propertyExpression.Expression, level);
            }
            else if (expression.NodeType == ExpressionType.Add)
            {
                Console.WriteLine($"{level}Extracting  Binary operation....");
                var binaryExpression = (BinaryExpression)expression;

                Console.WriteLine($"{level}Left Operand: {binaryExpression.Left} - Right Operand: {binaryExpression.Right}");

            }
            else if(expression.NodeType == ExpressionType.New)
            {
                Console.WriteLine($"{level}Extracting Object Creation....");
                var newExpression = (NewExpression)expression;

                Console.WriteLine($"{level}Constructor: {newExpression.Constructor.DeclaringType.Name}");

                foreach (var argiment in newExpression.Arguments)
                {
                    ParseExpression(argiment, level);
                }
            }
            else if(expression.NodeType == ExpressionType.MemberInit)
            {
                Console.WriteLine($"{level}Extracting Object Creation with Members....");
                var memberInitExpr = (MemberInitExpression)expression;

                ParseExpression(memberInitExpr.NewExpression, level);

                foreach (var memberBinding in memberInitExpr.Bindings)
                {
                    Console.WriteLine($"{level}Extracting Member....");
                    Console.WriteLine($"{level}Member: {memberBinding.Member.Name}");

                    var memberAssignment = (MemberAssignment)memberBinding;

                    ParseExpression(memberAssignment.Expression, level);
                }
            }
            else
            {
                //Variable
                //TODO: Extract not supported expression by compilation
            }
        }
    }
}
