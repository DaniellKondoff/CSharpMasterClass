namespace ObjectCreationWithCtors
{
    class Program
    {
        static void Main(string[] args)
        {
            var catType = typeof(Cat);
            var cat = catType.CreateInstance<Cat>();
            var cat1 = (Cat)ObjectFactory.CreateInstance(catType, "Test Cat");
            var cat2 = (Cat)ObjectFactory.CreateInstance(catType, "Test Cat", 2);
            var cat3 = (Cat)ObjectFactory.CreateInstance(catType, "Test Cat 2", 3);

            var catt = ObjectFactory.CreateInstanceByT<Cat, string>(catType, "BY Type");
            System.Console.WriteLine(catt.Name);
        }
    }
}
