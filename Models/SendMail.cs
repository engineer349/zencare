using System.Net.Mail;
using System.Net;
using Twilio.Http;
using System.Net.Mime;
using System.Text;

namespace Zencareservice.Models
{
    public class SendMail
    {
     
        public string EmailSend(string From, string To, string Pass, string Subject, string Mailbody, string host, int port)
        {
            try
            {
                MailMessage message = new MailMessage();
                //SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(From);
                message.To.Add(new MailAddress(To));
                
                message.Subject = Subject;
                message.IsBodyHtml = true; 
                message.Body = Mailbody;
                //MailMessage mailMessage = new MailMessage("zenhealthcareservice@gmail.com",Email, "Autoverification", "Your Zencareservice signup Account OTP verification of Email is " + randomCode);

                // Create an alternate view with HTML content

                //using (System.IO.StreamReader reader = new System.IO.StreamReader("wwwroot/code/SubjectMail.html"))
                //{
                //    htmlContent = reader.ReadToEnd();
                //}
                //Random random = new Random();

 

              //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlContent, Encoding.UTF8, MediaTypeNames.Text.Html);
               //message.AlternateViews.Add(htmlView);

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
