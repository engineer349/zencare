using System.Net.Mail;
using System.Net;

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
                message.IsBodyHtml = true; //to make message body as html
                message.Body = Mailbody;

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
                return ("Failed");
            }
            return ("Success");
        }
    }
}
