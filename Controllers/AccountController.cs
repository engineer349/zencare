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
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.CodeDom.Compiler;

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

      

        

        public IActionResult VerifyOtp()
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

            if (TempData.TryGetValue("GOTP", out var gotp))
            {
              
                ViewBag.Message = gotp;

                string _genotp = Convert.ToString(ViewBag.Message);

                if (Convert.ToInt64(enteredOtp) == Convert.ToInt64(_genotp))
                {
                    
                    return RedirectToAction("Index", "Account");
                }
              
            }

            return View("Login");
           
        }


        private List<Signup> roles = new List<Signup>
        {
            new Signup { RoleId = "patient", RoleName = "Patient" },
            new Signup { RoleId = "doctor", RoleName = "Doctor" },
            new Signup { RoleId = "staff", RoleName = "Staff" },
        };


        public IActionResult Register()
        {
            string returnUrl = "/Account/Register";
            ViewBag.Roles = new SelectList(roles, "RoleId", "RoleName");
          // Set your desired return URL
            ViewData["ReturnUrl"] = returnUrl;
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
                
                if (!string.IsNullOrEmpty(ResetPassword) && !string.IsNullOrEmpty(ConfirmResetPassword))
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
                string generatedCode = Codegenerator();
                _generatedOtp = Convert.ToInt32(generatedCode);
                SendMail sendMail = new SendMail();
                SmtpClient client = new SmtpClient();
                SendMail.EmailSend("zenhealthcareservice@gmail.com", obj.Email, "lamubclwmhfjwjjs", "Autoverification", "Your Zencareservice signup Account OTP verification of Email is "+ +_generatedOtp, "smtp.gmail.com", 587);
                return RedirectToAction("Login", "Account");
            }

            return View();
         
            
        }

      
       

        public IActionResult Login() 
        {
            return View();
        }
       
        public IActionResult ResendEmail(Signup Obj)
        {
            string generatedCode = Codegenerator();
            _generatedOtp = Convert.ToInt32(generatedCode);

            if (TempData.TryGetValue("MyEmail", out var myEmail))
            {

                ViewBag.Message = myEmail;

                Obj.Email = Convert.ToString(ViewBag.Message);

                SendMail sendMail = new SendMail();
                SmtpClient client = new SmtpClient();

                string mail = sendMail.EmailSend("zenhealthcareservice@gmail.com", Obj.Email, "lamubclwmhfjwjjs", "Autoverification", "Your Zencareservice signup Account OTP verification of Email is " + +_generatedOtp, "smtp.gmail.com", 587);

                return View("VerifyOtp", "Account");
            }
            else
            {
                return RedirectToAction("Register", "Account");
            }

        }

        private bool IsDateOfBirthValid(DateTime dob)
        {
            // Ensure the user is at least 18 years old
            return dob.AddYears(18) <= DateTime.Now && dob > DateTime.Now.AddYears(-100); // Assuming a reasonable upper limit for age
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        private string Codegenerator()
        {
            Random random = new Random();
            int randomCode = random.Next(10000, 100000);
            _generatedOtp = randomCode;
            TempData["GOTP"] = _generatedOtp;
            return _generatedOtp.ToString();

        }
        

        [HttpPost]
        public IActionResult Register(Signup Obj, string returnUrl)
        {
            ViewBag.Roles = new SelectList(roles, "RoleId", "RoleName");
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
               
                // Perform additional validation
                if (IsDateOfBirthValid(Obj.Dob))
                {
                    bool agreeToTerms = true;

                    if (agreeToTerms == true)
                    {
                       

                        if (IsValidEmail(Obj.Email))
                        {

                            try
                            {
                               
                                string SelectedRoleId = Obj.RoleId;                            
                                int agreeterms = Convert.ToInt32(Obj.agreeterm);
                                string fname = Obj.Firstname;
                                string lname = Obj.Lastname;
                                string password = Obj.Password;
                                string confirmpassword = Obj.Confirmpassword;
                                string username = Obj.Username;
                                string phoneno = Obj.Phonenumber;
                                string validemail = Obj.Email;
                                TempData["MyEmail"] = validemail;
                                DateTime Dob = Obj.Dob;
                                Obj.Status = 1;
                              
                                if (!String.IsNullOrEmpty(validemail))
                                {

                                    string generatedCode = Codegenerator();
                                    _generatedOtp = Convert.ToInt32(generatedCode);
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
                                <p>Thank you for using Zenhealthcareservice! </p>
                                <p>To ensure the security of your account, we have generated a One-Time Password (OTP) for you.</p>
                                <p class=""verification-code"">Your Verification Code:{_generatedOtp}</p>
                                <p class=""verification-instructions"">Please use the above code to verify your email address.</p>
        
                            </div>
                        </body>
                        </html>", "smtp.gmail.com", 587);

                                if (mail == "Success")
                                {
                                    DataAccess Obj_DataAccess = new DataAccess();
                                    DataSet ds = new DataSet();
                                    ds = Obj_DataAccess.SaveRegister(Obj);


                                    return RedirectToAction("VerifyOtp", "Account");
                                }

                            }
                            catch (Exception ex)
                            {
                                string msg = ex.Message.ToString();
                                ViewBag.Message = msg;
                            }
                        }
                        else
                        {
                            TempData["Email"] = "InvalidUser";
                            return View();

                        }

                    }
                    else
                    {
                        ModelState.AddModelError(nameof(Signup.agreeterm), "Pls  agree to terms of service and condition.");
                    }

                }
                else
                {
                    ModelState.AddModelError(nameof(Signup.Dob), "User must be at least 18 years old.");
                }
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }


      
            return View();
              

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
                        string Role = ds.Tables[0].Rows[0]["Role"].ToString();
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
                        Response.Cookies.Append("Role", UserName, options);
                        CookieOptions options2 = new CookieOptions();
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
