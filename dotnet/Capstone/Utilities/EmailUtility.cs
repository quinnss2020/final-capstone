using Capstone.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.IO;

public class EmailUtility
{
    public bool SendConfirmEmail(Email emailModel)
    {
        try
        {
            string directory = Environment.CurrentDirectory;
            string filename = "Confirmation.html";

            // Create the full path
            string fullPath = Path.Combine(directory, filename);


            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("deltastoragesolutions@outlook.com"));
            email.To.Add(MailboxAddress.Parse(emailModel.ToAccount));
            email.Subject = emailModel.Subject;
            string text = File.ReadAllText(fullPath).Replace("999999", emailModel.MessageBody);
            email.Body = new TextPart(TextFormat.Html) { Text = text };

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

    public bool SendCheckoutEmail(string userEmail, Unit unit, string code)
    {
        string subject = $"Congrats From DSS! You placed the winning bid!";

        try
        {

            string directory = Environment.CurrentDirectory;
            string filename = "Winning.html";

            // Create the full path
            string fullPath = Path.Combine(directory, filename);


            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("deltastoragesolutions@outlook.com"));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = subject;
            string text = File.ReadAllText(fullPath).Replace("AA0000", code).Replace("Columbus", unit.City).Replace("101", unit.LocalId.ToString());

            email.Body = new TextPart(TextFormat.Html) { Text = text };


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

    public string OrderNumberGenerator()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Random rand = new Random();
        int num = rand.Next(0, chars.Length -1);
        string letter1 = chars[num].ToString();      
        num = rand.Next(0, chars.Length - 1);
        string letter2 = chars[num].ToString();
        string letters = letter1 + letter2;
        rand = new Random();
        return letters += rand.Next(0, 10000).ToString("0000");
    }
    

    public Email FormEmail(string toEmail, string subject, string body)
    {
        Email email = new Email();
        email.ToAccount = toEmail;
        email.Subject = subject;
        email.MessageBody = body;

        return email;
    }

    //private str method html data params stringify each line, WWJD: into text editor, use reg expression to get rid of enters 2) go look at reading lecture final for replace 
}

