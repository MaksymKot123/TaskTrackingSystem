using System;
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

        public static void SendEmail(string clientEmail, Status status)
        {
            var from = new MailAddress(MailAddress, EmailSenderName);
            var to = new MailAddress(clientEmail);

            var mail = new MailMessage(from, to)
            {
                Subject = EmailSubject,
                IsBodyHtml = true
            };

            switch (status)
            {
                case Status.Completed:
                    mail.Body = GetGreeting() + CompletedProjectText() + EndOfTheMail();
                    break;
                case Status.OnProgress:
                    mail.Body = GetGreeting() + OnProgresProjectText() + EndOfTheMail();
                    break;
                case Status.Started:
                    mail.Body = GetGreeting() + StartedProjectText() + EndOfTheMail();
                    break;
                default:
                    mail.Body = GetGreeting() + EndOfTheMail();
                    break;
            }

            var smtp = new SmtpClient(SmtpAddress, SmtpPort)
            {
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(MailAddress, MailPassword),
                EnableSsl = true
            };
            smtp.Send(mail);
        }

        private static string GetGreeting()
        {
            return @"<h3>Dear client</h3>";
        }

        private static string StartedProjectText()
        {
            return @"<p>We have got recently your project. We will keep you
            informed about it status.</p>";
        }

        private static string OnProgresProjectText()
        {
            return @"<p>Your project is almost completed</p>";
        }

        private static string CompletedProjectText()
        {
            return @"<p>We are happy to tell you that your project is completed.</p>
            <p>We will contact you as soon as possible.</p>";
        }

        private static string EndOfTheMail()
        {
            return $@"<p>Best regards,</p>
                    <p>{EmailSenderName}</p>";
        }
    }
}
