using WebAppExpressions.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebAppExpressions.Controllers
{
    public class AnotherController : Controller
    {
        public IActionResult About()
        {
            return this.RedirectTo<HomeController>(c => c.Index());
        }
    }
}
