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
            string UsrId = Request.Cookies["UsrId"];

            string UsrName = Request.Cookies["UsrName"];
            if (string.IsNullOrEmpty(UsrId) || string.IsNullOrEmpty(UsrName))
            {
                return RedirectToAction("Login","Account");
            }
            return View();
        }
    }
}
