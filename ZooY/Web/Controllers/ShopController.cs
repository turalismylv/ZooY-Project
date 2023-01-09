using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
