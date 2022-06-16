using WebAppExpressions.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebAppExpressions.Controllers
{
    public class AnotherController : Controller
    {
        public IActionResult About()
        {
            var id = 5;

            return this.RedirectTo<HomeController>(c => c.Index(id, "MyApp"));
        }
    }
}
