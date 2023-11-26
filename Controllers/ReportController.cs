using Microsoft.AspNetCore.Mvc;

namespace Zencareservice.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }
        public IActionResult Dashboard()
        {
          
            return View();
        }
    }
}
