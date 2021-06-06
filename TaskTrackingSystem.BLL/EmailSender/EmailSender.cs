using System.Net;
using System.Net.Mail;
using TaskTrackingSystem.DAL.Enums;

namespace TaskTrackingSystem.BLL.EmailSender
{
    /// <summary>
    /// Static class, which sends email to client when project's status 
    /// were updated 
    /// </summary>
    public static class EmailSender
    {
        const string MailAddress = "tasktrackingsystememailsender@gmail.com";
        const string MailPassword = "qwe123Aa";
        const string SmtpAddress = "smtp.gmail.com";
        const int SmtpPort = 587;
        const string EmailSenderName = "John Doe";
        const string EmailSubject = "Status of your project";

        const string Greeting = @"<h3>Dear client</h3>";
        const string OnProgresText = @"<p>Your project is almost completed</p>";
        const string EmailEnd = "<p>Best regards,</p><p>"+ EmailSenderName + "</p>";
        const string CompletedProjectText = @"<p>We are happy to tell you that your
            project is completed.</p><p>We will contact you as soon as possible.</p>";
        const string StartedProjectText = @"<p>We have got recently your project. We 
            will keep you informed about it status.</p>";

        public static void SendEmail(string clientEmail, Status status)
        {
            var from = new MailAddress(MailAddress, EmailSenderName);
            var to = new MailAddress(clientEmail);

            var mail = new MailMessage(from, to)
            {
                Subject = EmailSubject,
                IsBodyHtml = true, 
                Body = status switch
                {
                    Status.Completed => Greeting + CompletedProjectText + EmailEnd,
                    Status.OnProgress => Greeting + OnProgresText + EmailEnd,
                    Status.Started => Greeting + StartedProjectText + EmailEnd,
                    _ => Greeting + EmailEnd
                }
            };

            var smtp = new SmtpClient(SmtpAddress, SmtpPort)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(MailAddress, MailPassword),
                EnableSsl = true
            };
            smtp.Send(mail);
        }
    }
}
