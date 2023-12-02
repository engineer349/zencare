﻿using Microsoft.AspNetCore.Mvc;
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
using System.Net;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace Zencareservice.Controllers
{
    public class AccountController : Controller
    {

        //private readonly TwilioService _twilioService;


        private int _generatedOtp;

        private string ResetEmail;

        public IActionResult Index()
        {

            ViewBag.Message = "Your Details are successfully saved!";

            return View("RegistrationSuccess", "Account");
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

        public IActionResult VerifyEmail(Signup Obj)
        {

                string validemail = Obj.Email;

                string email = "example@example.com";

                string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

                Regex regex = new Regex(emailPattern);

               if (validemail != null && regex.IsMatch(email))
               {
                    TempData["Email"] = "Validuser";

                return RedirectToAction("Register", "Account");
               }
                
              else
              {
                TempData["Email"] = "Validuser";

               }
                return View("Register");

 

        }

        public IActionResult VerifyOtp(Signup Obj)
        {
            return View();
        }

     
        public IActionResult ValidateOtp(Signup model)
        {

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(model.numeric1);
            stringBuilder.Append(model.numeric2);
            stringBuilder.Append(model.numeric3);
            stringBuilder.Append(model.numeric4);
            stringBuilder.Append(model.numeric5);
          

            string enteredOtp = stringBuilder.ToString();


            //string enteredOtp = model.numeric1 +""+model.numeric2+ ""+model.numeric3+""+model.numeric4 +""+model.numeric5;

 
            if (TempData.TryGetValue("MyData", out var myData))
            {
              
                ViewBag.Message = myData;

                string _genotp = Convert.ToString(ViewBag.Message);

                if (Convert.ToInt64(enteredOtp) == Convert.ToInt64(_genotp))
                {
                    return RedirectToAction("Index", "Account");
                }
              
            }

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

        public IActionResult ForgotPassword()
        {
            
            return View();
        }


        [HttpPost]
        public IActionResult ResetPassword(Signup Obj)
        {
            string ResetPassword = Obj.RPassword;
            string ConfirmResetPassword = Obj.CRPassword;
           
            if (TempData.TryGetValue("ResetData", out var myresetData))
            {
                ViewBag.Message = myresetData;
                
                string ResetEmail = Convert.ToString(ViewBag.Message);
            
                string AllData = Convert.ToString(ViewBag.AllData);
                
                if (!string.IsNullOrEmpty(Obj.RPassword) && !string.IsNullOrEmpty(Obj.CRPassword))
                {
                    DataAccess Obj_DataAccess = new DataAccess();
                    DataSet ds = new DataSet();
                    ds = Obj_DataAccess.ResetPassword(Obj, ResetEmail);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(Signup obj)
        {
            string ResetEmail = obj.Email;

            if(ResetEmail!=null)
            {
                TempData["ResetData"] = ResetEmail;
                
                return RedirectToAction("ResetPassword", "Account");
            }

            return View();
         
            
        }

       

        public IActionResult Login() 
        {
            return View();
        }

        public IActionResult ResendEmail(Signup Obj)
        {
           

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
            
            string fname = Obj.Firstname;
            string email = Obj.Email; 
            string lname = Obj.Lastname;
            string password = Obj.Password;
            string confirmpassword = Obj.Confirmpassword;
            string username = Obj.Username;
            string phoneno = Obj.Phonenumber;

            Random random = new Random();

            //// Generate a random 5-digit code
            ///
            if(!String.IsNullOrEmpty(email) )
            {

                int randomCode = random.Next(10000, 100000);
                _generatedOtp = randomCode;
                TempData["MyData"] = _generatedOtp;
               
            }
          
            SendMail sendMail = new SendMail();
            SmtpClient client = new SmtpClient();    
            string mail = sendMail.EmailSend("zenhealthcareservice@gmail.com", Obj.Email, "lamubclwmhfjwjjs", "Autoverification", $@"



<!DOCTYPE html>

<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Email Verification</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background-color: #f4f4f4;
        }}

        .logo {{
            max-width: 200px;
            height: auto;
            margin-bottom: 20px;
        }}

        .verification-container {{
            background-color: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            text-align: center;
        }}

        .verification-code {{
            font-size: 24px;
            font-weight: bold;
            color: #007bff;
            margin-bottom: 20px;
        }}

        .verification-instructions {{
            color: #555;
            margin-bottom: 20px;
        }}

        .verification-btn {{
            padding: 10px;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }}
    </style>
</head>
<body>
    <img src=""~/images/zencare-logo1.png"" alt=""Your Logo"" class=""logo"">
    <div class=""verification-container"">
        
        <h2>Hi {fname},</p>
        <p>Thank you for using Zenhealthcareservice! To ensure the security of your account, we have generated a One-Time Password (OTP) for you.</p>
        <p class=""verification-code"">Your Verification Code:{_generatedOtp}</p>
        <p class=""verification-instructions"">Please use the above code to verify your email address.</p>
        
    </div>
</body>
</html>" , "smtp.gmail.com", 587);

            if (mail =="Success")
            {

                Obj.Status = 1;
                Obj.Role = "Patient";
                DataAccess Obj_DataAccess = new DataAccess();
                DataSet ds = new DataSet();
                ds = Obj_DataAccess.SaveRegister(Obj);

                return RedirectToAction("VerifyOtp", "Account");
            }
                
                return RedirectToAction("Login", "Account");
              

        }


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
