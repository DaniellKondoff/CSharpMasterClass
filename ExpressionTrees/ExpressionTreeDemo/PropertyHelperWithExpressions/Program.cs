using System.Collections.Generic;
using System.Linq;

namespace PropertyHelperWithExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new { id = 5, query = "Text" };
            var dict = new Dictionary<string, object>();

            PropertyHelper
                .GetProperties(obj.GetType())
                .Select(pr => new
                {
                    pr.Name,
                    Value = pr.Getter(obj)
                })
                .ToList()
                .ForEach(pr => dict[pr.Name] = pr.Value);
        }
    }
}
