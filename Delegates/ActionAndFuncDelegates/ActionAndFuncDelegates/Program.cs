using System;

namespace ActionAndFuncDelegates
{
    public class Program
    {
        static void Main(string[] args)
        {
            Action action = SomeMethod;
            action += () => Console.WriteLine("Else");           
            //action();


            Action<int> actionInt = SomeMethod;
            actionInt += x => Console.WriteLine(x);
            //actionInt(5);

            Func<int> func = SomeIntMethod;
            func += () => 42;

            Console.WriteLine(func());

            Func<string, bool, int> func2 = SomeIntMethod;
            Console.WriteLine(func2("Testing", true));

            Func<int, int, int> sumFunc = (x, y) => x + y;
            Console.WriteLine(sumFunc(5, 10));

            Action<Person> personAction = person => person.SaySomething();
            var person = new Person();
            person.Name = "Test Name";
            personAction(person);

            Func<Person, string> personFunc = person => person.Name;
            Console.WriteLine(personFunc(person));

            Calculate(person => person.Name);
            Calculate(person => person.Name + " IS Cool!");

            Func<Person, string> personFunc2 = person => person.Name + " IS Cool!";
            Console.WriteLine(personFunc2(person));
        }

        public static void SomeMethod()
        {
            Console.WriteLine("Test");
        }

        public static void SomeMethod(int number)
        {
            Console.WriteLine(number);
        }

        public static int SomeIntMethod()
        {
            Console.WriteLine("Caling");
            return 420;
        }

        public static int SomeIntMethod(string text, bool someBool)
        {
            Console.WriteLine("Caling." + text);
            return 420;
        }

        public static void Calculate(Func<Person, string> func)
        {
            var person = new Person();
            person.Name = "Test";

            Console.WriteLine(func(person));
        }
    }
}
