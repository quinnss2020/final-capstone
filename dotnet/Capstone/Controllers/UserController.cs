﻿using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Capstone.Controllers
{

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserDao userDao;
        private readonly ITempUserDao tempUserDao;

        public UserController(
            ITokenGenerator tokenGenerator,
            IPasswordHasher passwordHasher,
            IUserDao userDao,
            ITempUserDao tempUserDao
            )
        {
            this.tokenGenerator = tokenGenerator;
            this.passwordHasher = passwordHasher;
            this.userDao = userDao;
            this.tempUserDao = tempUserDao;
        }

        //POST /login
        [HttpPost("/login")]
        public IActionResult Authenticate(LoginUser userParam)
        {
            // Default to bad username/password message
            IActionResult result = Unauthorized(new { message = "Username or password is incorrect." });

            User user;
            // Get the user by username
            try
            {
                //check if user in is users table
                user = userDao.GetUserByEmail(userParam.Email);
                if (user != null && user.Confirmed && passwordHasher.VerifyHashMatch(user.PasswordHash, userParam.Password, user.Salt))
                {
                    string token = tokenGenerator.GenerateToken(user.Id, user.Email, user.Role);

                    // Create a ReturnUser object to return to the client
                    LoginResponse retUser = new LoginResponse() { User = new ReturnUser() { Id = user.Id, Email = user.Email, Role = user.Role }, Token = token };

                    // Switch to 202 OK
                    result = Accepted(retUser);

                    return result;
                }
                else if (user != null && !user.Confirmed && passwordHasher.VerifyHashMatch(user.PasswordHash, userParam.Password, user.Salt))
                {
                    string token = tokenGenerator.GenerateToken(user.Id, user.Email, user.Role);

                    // Create a ReturnUser object to return to the client
                    LoginResponse retUser = new LoginResponse() { User = new ReturnUser() { Id = user.Id, Email = user.Email, Role = user.Role }, Token = token };

                    return Ok(retUser);
                }
            }
            catch (DaoException)
            {
                // return default Unauthorized message instead of indicating a specific error
                return result;
            }

            return result;
        }

        //POST /register
        [HttpPost("/register")]
        public IActionResult Register(RegisterUser userParam)
        {
            // Default generic error message
            const string ErrorMessage = "An error occurred and user was not created.";

            IActionResult result = BadRequest(new { message = ErrorMessage });

            // is username already taken
            try
            {
                User existingUser = userDao.GetUserByEmail(userParam.Email);
                if (existingUser != null)
                {
                    return Conflict(new { message = "Email already registered. Please login." });
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }

            // create new user
            User newUser;
            try
            {
                string codeString = VerificationCodeGenerator();
                userParam.Code = codeString;
                newUser = userDao.CreateUser(userParam);
            }
            catch (DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }

            if (newUser != null)
            {
                // Create a ReturnUser object to return to the client
                ReturnUser returnUser = new ReturnUser() { Id = newUser.Id, Email = newUser.Email, Role = newUser.Role };

                result = Created("/login", returnUser);
            }

            return result;
        }

        [Authorize]
        [HttpPut("/login/confirm")]
        public IActionResult ConfirmUser(LoginUser user, string code)
        {
            const string ErrorMessage = "An error occurred and user was not confirmed.";
            User confirmedUser;
            try
            {
                confirmedUser = userDao.GetUserByEmail(user.Email);
                if(confirmedUser.Code == code)
                {
                    confirmedUser.Confirmed = true;
                    string token = tokenGenerator.GenerateToken(confirmedUser.Id, confirmedUser.Email, confirmedUser.Role);

                    // Create a ReturnUser object to return to the client
                    LoginResponse retUser = new LoginResponse() { User = new ReturnUser() { Id = confirmedUser.Id, Email = confirmedUser.Email, Role = confirmedUser.Role }, Token = token };

                    return Ok(retUser);

                }
                
            }
            catch(DaoException)
            {
                return StatusCode(500, ErrorMessage);

            }

            return Unauthorized();
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

        private string VerificationCodeGenerator()
        {
            Random rand = new Random();

            return rand.Next(0, 1000000).ToString("000000");
        }
    }
}
