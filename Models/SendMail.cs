using System.Net.Mail;
using System.Net;
using Twilio.Http;
using System.Net.Mime;
using System.Text;

namespace Zencareservice.Models
{
    public class SendMail
    {
        string _generatedOtp;
        public string EmailSend(string From, string To, string Pass, string Subject, string Mailbody, string host, int port)
        {
            try
            {
                MailMessage message = new MailMessage();
                //SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(From);
                message.To.Add(new MailAddress(To));
                
                message.Subject = Subject;
                message.IsBodyHtml = true; //to make message body as html
                message.Body = Mailbody;
                //MailMessage mailMessage = new MailMessage("zenhealthcareservice@gmail.com",Email, "Autoverification", "Your Zencareservice signup Account OTP verification of Email is " + randomCode);

                // Create an alternate view with HTML content

                //using (System.IO.StreamReader reader = new System.IO.StreamReader("wwwroot/code/SubjectMail.html"))
                //{
                //    htmlContent = reader.ReadToEnd();
                //}
                //Random random = new Random();

                //// Generate a random 5-digit code
                //int randomCode = random.Next(10000, 100000);
                //_generatedOtp = Convert.ToString( randomCode);

                
                // Attach the alternate view to the email
               

                string htmlContent = $@"
@*
    For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
*@


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
        <p class=""verification-code"">Your Verification Code:{_generatedOtp}</p>
        <p class=""verification-instructions"">Please use the above code to verify your email address.</p>
        <button class=""verification-btn"" href=""https://www.google.com"">Verify Email</button>
    </div>
</body>
</html>


";
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlContent, Encoding.UTF8, MediaTypeNames.Text.Html);
                message.AlternateViews.Add(htmlView);

                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Credentials = new NetworkCredential(From, Pass);
                   
                    smtpClient.Send(message);

                    
                  
                }

            }
            catch (Exception ex)
            {
                throw ex;
             

            }
            return ("Success");
        }
    }
}
