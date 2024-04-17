﻿using Capstone.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;

 public class EmailUtility
{
    public bool SendConfirmEmail(Email emailModel)
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

    public bool SendCheckoutEmail(string userEmail, Unit unit, string code)
    {
        string subject = $"Congrats From DSS! You placed the winning bid";
        string messageBody = $"Congratulations! You have won an auction! \n Your unit is number {unit.LocalId} and is located in {unit.City}. \nYour confirmation code is {code}.";


        try
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("deltastoragesolutions@outlook.com"));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = $"<h1>{messageBody}</h1>" };


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
}

