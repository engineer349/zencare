using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Zencareservice.Controllers
{
    public class Prescriptions : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Prescrt()
        {

            return View();
        }
        public IActionResult Prescedit()
        {

            return View();
        }

        public IActionResult Presclist() 
        {
            return View();
        }
    }
}
