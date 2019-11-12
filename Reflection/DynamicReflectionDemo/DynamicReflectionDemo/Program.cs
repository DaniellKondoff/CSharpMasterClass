using System;
using System.Linq;
using System.Reflection;

namespace DynamicReflectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.Load(new AssemblyName("CoolLibrary"));

            var type = assembly.GetTypes().FirstOrDefault(t => t.Name == "CoolClass");

            //var methodInfo = type.GetMethod("CoolMethod");
            //methodInfo.Invoke(null, new object[0]);


            dynamic someCoolClass = new ExposedObject(type);

            someCoolClass.CoolMethod();

            someCoolClass.Name = "Test";

            Console.WriteLine(someCoolClass.Name);
        }
    }
}
