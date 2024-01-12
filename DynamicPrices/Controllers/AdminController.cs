using Microsoft.AspNetCore.Mvc;

namespace DynamicPrices.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }
    }
}
