﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Net.Mail;
using System.Reflection;
using Zencareservice.Models;
using Zencareservice.Repository;

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
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult VerifyOtp(Signup Obj)
        {
            return View();
        }

        public IActionResult ValidateOtp(Signup Obj)
        {
            if (Obj.Randomcode == ViewBag.Random)
            {
                // OTP is correct, proceed with registration

                return View("RegistrationSuccess", "Account");
            }
          
            return View();
        }
           
           
      
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ResetPassword()
        {
            return View();
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

            SendMail sendMail = new SendMail();
            SmtpClient client = new SmtpClient();       
            string mail = sendMail.EmailSend("zenhealthcareservice@gmail.com", Obj.Email, "lamubclwmhfjwjjs", "Autoverification", "Your Zencareservice signup Account OTP verification of Email is "+ + randomCode, "smtp.gmail.com", 587);
            return View("VerifyOtp", "Account");
        }


        [HttpPost]
        public IActionResult Register(Signup Obj)
        {
                Random random = new Random();

                // Generate a random 5-digit code
                int randomCode = random.Next(10000, 100000);
                string email = Obj.Email;
                SendMail sendMail = new SendMail();
                SmtpClient client = new SmtpClient();
                string mail = sendMail.EmailSend("zenhealthcareservice@gmail.com", Obj.Email, "lamubclwmhfjwjjs", "Autoverification", "Your Zencareservice signup Account OTP verification of Email is" + +randomCode, "smtp.gmail.com", 587);
                
               if (Obj.Email != null)
                {


                string fname = Obj.Firstname;
                string lname = Obj.Lastname;

                string password = Obj.Password;
                string confirmpassword = Obj.Confirmpassword;
                string username = Obj.Username;
                string phoneno = Obj.Phonenumber;
                Obj.Status = 1;
                Obj.Role = "Patient";

                return RedirectToAction("VerifyOtp", "Account");

                }
                
                DataAccess Obj_DataAccess = new DataAccess();
                DataSet ds = new DataSet();
                ds = Obj_DataAccess.SaveRegister(Obj);

                return RedirectToAction("Login", "Account");
               
            // Generate OTP and send it to the user's mobile phone
            // string generatedOtp = _twilioService.SendOtp(Obj.Phonenumber);

            // Save the generatedOtp to associate it with the user's profile

            // Pass the generatedOtp to the verification view
            // return RedirectToAction("VerifyOtp", new { generatedOtp });



            //dataSet = Obj_DataAccess.SaveRegister(password);
            //dataSet = Obj_DataAccess.SaveRegister(confirmpassword);
            //dataSet = Obj_DataAccess.SaveRegister(phoneno);
            //dataSet = Obj_DataAccess.SaveRegister(username);
            //dataSet = Obj_DataAccess.SaveRegister(email);
          
           

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
