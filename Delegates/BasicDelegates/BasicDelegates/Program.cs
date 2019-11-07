using System;

namespace BasicDelegates
{
    public class Program
    {
        static void Main(string[] args)
        {
            MyVoidDelegate myDeleg = new MyVoidDelegate(PrintInteger);
            myDeleg += SomeMethod;
            //MyVoidDelegate myDeleg2 = new MyVoidDelegate(SomeMethod);
            myDeleg(100);
            myDeleg?.Invoke(100);
            myDeleg -= SomeMethod;

            MyStringDelegateByIntegers del3 = new MyStringDelegateByIntegers(SomeOtherMethod);
            PassSomeDelegate(myDeleg);
            Console.WriteLine(del3(5,6));

            var person = new Person();
            myDeleg += person.SomePersonMethod;
            Console.WriteLine(myDeleg.Target?.GetType().Name);
            
        }

        public static void PassSomeDelegate(MyVoidDelegate del)
        {
            del?.Invoke(5);
        }

        public static void PassSomeDynamicDelegate(Delegate del)
        {
            del.DynamicInvoke(5);
        }


        public static void PrintInteger(int number)
        {
            Console.WriteLine(number);
        }

        public static void PrintString(string text)
        {
            Console.WriteLine(text);
        }

        public static void SomeMethod(int myInt)
        {
            Console.WriteLine(myInt + 10);
        }

        public static string SomeOtherMethod(int x, int y)
        {
            return x + y.ToString();
        }
    }
}
