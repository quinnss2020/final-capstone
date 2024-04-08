using System.Net.Mail;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using Capstone.DAO;

namespace Capstone.Controllers
{
 
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ITempUserDao tempUserDao;

        //POST /auth
        [HttpPost("/auth/user")]
        public IActionResult SendVerificationEmail(ReturnUser userParam)
        {
            RegisterUser user = tempUserDao.GetTempUserById(userParam.Id);
            string codeString = user.Code;
            Email email = new Email();
            email.ToAccount = userParam.Email;
            email.Subject = "Account Verification";
            email.MessageBody = $"Use this code to verify your account: {codeString}";
            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("deltastoragesolutions", "DeltaStoragePassword");

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress("deltastoragesolutions@gmail.com"),
                        Subject = email.Subject,
                        Body = email.MessageBody
                    };
                    mailMessage.To.Add(email.ToAccount);
                    client.Send(mailMessage);
                }
                return Ok("Email was sent");
            }
            catch (Exception ex)
            {
                // return default Unauthorized message instead of indicating a specific error
                return StatusCode(500, $"Email could not be sent: {ex.Message}");
            }
        }
        //GET /whoami
        [HttpGet("/whoami")]
        public ActionResult<string> WhoAmI()
        {
            string result = User.Identity.Name;
            if (result == null)
            {
                return "No token provided.";
            }
            else
            {
                return result;
            }
        }


        //GET /admin/confirm
        [Authorize]
        [HttpGet("confirm")]
        public ActionResult<string> Confirm()
        {
            
            return Ok($"A valid token was received.");
        }

    }
}
