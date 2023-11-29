using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Template;
using System.Data;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using Twilio.TwiML.Messaging;
using Zencareservice.Models;
using Zencareservice.Repository;
using System;
using System.IO;

namespace Zencareservice.Controllers
{
    public class AccountController : Controller
    {

        //private readonly TwilioService _twilioService;

        //public AccountController(IConfiguration configuration)
        //{
        //    var twilioConfig = configuration.GetSection("Twilio");
        //    _twilioService = new TwilioService(
        //        twilioConfig["AccountSid"],
        //        twilioConfig["AuthToken"],
        //        twilioConfig["PhoneNumber"]
        //    );
        //}
        private int _generatedOtp;
        
        public IActionResult Index()
        {
            return View();
        }

        public bool IsOtpValid(string enteredOtp)
        {
            // Check if the entered OTP is a valid integer
            if (int.TryParse(enteredOtp, out int enteredOtpValue))
            {
                // Compare with the originally generated OTP
                return enteredOtpValue == _generatedOtp;
            }

            return false;
        }

        public IActionResult VerifyOtp(Signup Obj)
        {
            return View();
        }

     
        public IActionResult ValidateOtp(Signup model)
        {
          
            string enteredOtp = model.numeric1 +""+model.numeric2+ ""+model.numeric3+""+model.numeric4 +""+model.numeric5;

            string _genotp = ViewData["key"].ToString();

            if(Convert.ToInt64(enteredOtp) ==  _generatedOtp)
            {
                return View("Login", "Account");
            }

            // OTP is not valid, handle accordingly
            return View("Login");
           
        }
           
        
   
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ResetPassword()
        {
           
            return View();
        }
        private IActionResult PasswordConfirmation()
        {

            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(Signup Obj)
        {
            string ResetEmail = Obj.Email;

            if(ResetEmail!=null)
            {
                return View("PasswordConfirmation","Account");
            }

            return View("Index", "Home");
         
            
        }

        public IActionResult Login() 
        {
            return View();
        }

        public IActionResult ResendEmail(Signup Obj)
        {
            Register();

            Random random = new Random();
            // Generate a random 5-digit code
            int randomCode = random.Next(10000, 100000);
            _generatedOtp = randomCode;
          

            SendMail sendMail = new SendMail();
            SmtpClient client = new SmtpClient();
           
            string mail = sendMail.EmailSend("zenhealthcareservice@gmail.com", Obj.Email, "lamubclwmhfjwjjs", "Autoverification", "Your Zencareservice signup Account OTP verification of Email is "+ + randomCode, "smtp.gmail.com", 587);
            
            return View("VerifyOtp", "Account");
        }

        public IActionResult SubjectMail()
        {
            return View();
        }
        

        [HttpPost]
        public IActionResult Register(Signup Obj)
        {
                //Random random = new Random();

                //// Generate a random 5-digit code
                //int randomCode = random.Next(10000, 100000);
                //_generatedOtp = randomCode;

               

                string email = Obj.Email;
                SendMail sendMail = new SendMail();
                SmtpClient client = new SmtpClient();    
                string mail = sendMail.EmailSend("zenhealthcareservice@gmail.com", Obj.Email, "lamubclwmhfjwjjs", "Autoverification", "Your Zencareservice signup Account OTP verification of Email is" , "smtp.gmail.com", 587);
                
               if (Obj.Email != null && mail =="Success")
                {


                string fname = Obj.Firstname;
                string lname = Obj.Lastname;

                string password = Obj.Password;
                string confirmpassword = Obj.Confirmpassword;
                string username = Obj.Username;
                string phoneno = Obj.Phonenumber;
                Obj.Status = 1;
                Obj.Role = "Patient";


                DataAccess Obj_DataAccess = new DataAccess();
                DataSet ds = new DataSet();
                ds = Obj_DataAccess.SaveRegister(Obj);

                return RedirectToAction("VerifyOtp", "Account");
            }
                
               

                return RedirectToAction("Login", "Account");
               
            // Generate OTP and send it to the user's mobile phone
            // string generatedOtp = _twilioService.SendOtp(Obj.Phonenumber);

            // Save the generatedOtp to associate it with the user's profile

            // Pass the generatedOtp to the verification view
            // return RedirectToAction("VerifyOtp", new { generatedOtp })
           

        }

     
       

        //[HttpGet]
        //public IActionResult VerifyOtp(string generatedOtp)
        //{
        //    // Pass the generatedOtp to the view for user verification
        //    var model = new VerifyOtpViewModel { GeneratedOtp = generatedOtp };
        //    return View(model);
        //}

        //[HttpPost]
        //public IActionResult VerifyOtp(VerifyOtpViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Verify the user-entered OTP
        //        bool isOtpValid = _twilioService.VerifyOtp(model.UserEnteredOtp);

        //        if (isOtpValid)
        //        {
        //            // OTP is valid, complete the registration process
        //            return RedirectToAction("RegistrationSuccess");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("UserEnteredOtp", "Invalid OTP");
        //        }
        //    }

        //    return View(model);
        //}

        public IActionResult RegistrationSuccess()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login Obj)
        {
            string username = Obj.Username;
            string password = Obj.Password;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {


                DataAccess Obj_DataAccess = new DataAccess();
                DataSet ds = new DataSet();
                ds = Obj_DataAccess.SaveLogin(Obj);

                int Status;

                if (ds.Tables[0].Rows.Count > 0)
                {

                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["LStatus"]);
                    if (Status == 1)
                    {
                        
                        string UsrId = ds.Tables[0].Rows[0]["RId"].ToString();
                        string UserName = ds.Tables[0].Rows[0]["Username"].ToString();
                        string Email = ds.Tables[0].Rows[0]["Email"].ToString();

                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(1), // Set the expiration date
                            HttpOnly = true, // Makes the cookie accessible only to the server-side code
                        };
                        Response.Cookies.Append("MyCookie", "CookieValue", cookieOptions);
                        Response.Cookies.Append("UserId", UsrId);
                        CookieOptions options = new CookieOptions();
                        options.Expires = DateTime.Now.AddMinutes(5);
                        Response.Cookies.Append("UsrName", UserName, options);

                        CookieOptions options1 = new CookieOptions();
                        options.Expires = DateTime.Now.AddMinutes(5);
                        Response.Cookies.Append("UsrId", UsrId, options1);

                        return RedirectToAction("Dashboard", "Report");


                    }
                }
            }
            else
            {
                return View();

            }

            return View(); 

        }
    }
}
