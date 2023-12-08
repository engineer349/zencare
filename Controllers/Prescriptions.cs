using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zencareservice.Models;

namespace Zencareservice.Controllers
{
    public class Prescriptions : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

      
        public IActionResult Prescrt()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Prescrt(Prescs Obj)
        {
            try
            {
                string pfname = Obj.PatientFirstName;
                string plname = Obj.PatientLastName;
                string dfname = Obj.DoctorFirstName;
                string dlname = Obj.DoctorLastName;
                string patage = Obj.PatientAge;
                string patgender = Obj.PatientGender;
                string patphoneno = Obj.PatientPhoneno;
                

            }
            catch(Exception ex)
            {
                throw ex;
            }


           
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
