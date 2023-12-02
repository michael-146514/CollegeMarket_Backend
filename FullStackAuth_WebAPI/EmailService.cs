using System;
using System.Net;
using System.Net.Mail;

namespace YourNamespace.Services
{
    public class EmailService
    {
        public void SendEmail(string senderEmail, string senderPassword, string receiverEmail, string subject, string body)
        {
            MailMessage mail = new MailMessage(senderEmail, receiverEmail);
            mail.Subject = subject;
            mail.Body = body;

            SmtpClient smtpClient = new SmtpClient("smtp.hostinger.com", 465); 
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            try
            {
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}

