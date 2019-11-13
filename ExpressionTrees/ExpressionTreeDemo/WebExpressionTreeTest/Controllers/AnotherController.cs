using Microsoft.AspNetCore.Mvc;

namespace WebExpressionTreeTest.Controllers
{
    public class AnotherController : Controller
    {
        public IActionResult SomeAction(int id)
        {
            return NotFound();
        }
    }
}
