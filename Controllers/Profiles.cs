using Microsoft.AspNetCore.Mvc;

namespace Zencareservice.Controllers
{
    public class Profiles : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
