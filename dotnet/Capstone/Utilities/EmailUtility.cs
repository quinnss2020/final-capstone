using Capstone.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;

 public class EmailUtility
{
    public bool SendVerificationEmail(Email emailModel)
    {
        try
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("deltastoragesolutions@outlook.com"));
            email.To.Add(MailboxAddress.Parse(emailModel.ToAccount));
            email.Subject = emailModel.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = $"<h1>{emailModel.MessageBody}</h1>" };

            //send email

            using var smtp = new SmtpClient();
            smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
            smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            smtp.Authenticate("deltastoragesolutions@outlook.com", "JakeQuinnSerinaTiana");
            smtp.Send(email);
            smtp.Disconnect(true);

            return true;
        }
        catch (SmtpCommandException ex)
        {
            Console.WriteLine("Email failed: " + ex);
            return false;
        }
    }

    public string VerificationCodeGenerator()
    {
        Random rand = new Random();

        return rand.Next(0, 1000000).ToString("000000");
    }

    public Email FormEmail(string toEmail, string subject, string body)
    {
        Email email = new Email();
        email.ToAccount = toEmail;
        email.Subject = subject;
        email.MessageBody = body;

        return email;
    }
}

