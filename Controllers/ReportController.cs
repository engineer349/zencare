using Microsoft.AspNetCore.Mvc;

namespace Zencareservice.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Dashboard()
        {
            var UserNamews = Request.Cookies["MyCookie"];
            string UserName = Request.Cookies["UsrId"].ToString();
            return View();
        }
    }
}
