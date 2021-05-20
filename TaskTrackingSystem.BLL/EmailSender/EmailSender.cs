using System;
using System.Net;
using System.Net.Mail;


namespace TaskTrackingSystem.BLL.EmailSender
{
    public static class EmailSender
    {
        const string MailAddress = "tasktrackingsystememailsender@gmail.com";
        const string MailPassword = "qwe123Aa";
        const string SmtpAddress = "smtp.gmail.com";
        const int SmtpPort = 587;
        const string EmailSenderName = "John Doe";

        public static void SendEmail(string clientEmail)
        {
            var from = new MailAddress(MailAddress, EmailSenderName);
            var to = new MailAddress(clientEmail);

            var mail = new MailMessage(from, to);


            var smtp = new SmtpClient(SmtpAddress, SmtpPort);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential(MailAddress, MailPassword);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
