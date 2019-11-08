using System;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            var cat = new Cat
            {
                Id = 1,
                Name = "Cat Name",
                Health = 100
            };

            cat.OnHealthChanged += CatOnHealthChanged;
            cat.OnHealthChanged += CatIsRemoved;

            cat.Health = 200;
        }

        private static void CatOnHealthChanged(object sender, int health)
        {
            var cat = sender as Cat;

            Console.WriteLine($"{cat.Name} has new Health: {health}");
        }

        private static void CatIsRemoved(object sender, int health)
        {
            var cat = sender as Cat;

            Console.WriteLine($"{cat.Name} has been removed");
        }
    }
}
