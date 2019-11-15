using Microsoft.AspNetCore.Mvc;
using WebExpressionTreeTest.Extensions;

namespace WebExpressionTreeTest.Controllers
{
    public class AnotherController : Controller
    {
        public IActionResult SomeAction(int id)
        {
            return NotFound();
        }

        public IActionResult About()
        {
            var id = 5;
            var query = "TestQuery";
            return this.RedirectTo<HomeController>(c => c.Index(id, query));
            //return this.RedirectTo(c => c.About());
            //return RedirectToAction("Index", "Home");
        }
    }
}
