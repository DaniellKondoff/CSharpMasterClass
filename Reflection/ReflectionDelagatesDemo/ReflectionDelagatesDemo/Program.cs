using System;
using System.Collections.Generic;
using System.Linq;

namespace ReflectionDelagatesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var homeController = new HomeController();
            var homeControolerType = homeController.GetType();

            var property = homeControolerType.GetProperties()
                .FirstOrDefault(pr => pr.IsDefined(typeof(DataAttribute), true));

            var getMethod = property.GetMethod;

            var dict = (IDictionary<string, object>)getMethod.Invoke(homeController, Array.Empty<object>());

            var deleg = (Func<HomeController, IDictionary<string, object>>)getMethod.CreateDelegate(typeof(Func<HomeController, IDictionary<string, object>>));

            var geleg2 = PropertyHelper<HomeController>.MakeFastPropertyGetter<IDictionary<string, object>>(property);
            

        }
    }
}
