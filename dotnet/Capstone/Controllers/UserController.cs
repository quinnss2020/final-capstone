using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                user = userDao.GetUserByEmail(userParam.Email);
            }
            catch (DaoException)
            {
                // return default Unauthorized message instead of indicating a specific error
                return result;
            }

            // If we found a user and the password hash matches
            if (user != null && passwordHasher.VerifyHashMatch(user.PasswordHash, userParam.Password, user.Salt))
            {
                // Create an authentication token
                string token = tokenGenerator.GenerateToken(user.Id, user.Email, user.Role);

                // Create a ReturnUser object to return to the client
                LoginResponse retUser = new LoginResponse() { User = new ReturnUser() { Id = user.Id, Email = user.Email, Role = user.Role }, Token = token };

                // Switch to 200 OK
                result = Ok(retUser);
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

            // is username already taken?
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
                newUser = tempUserDao.CreateUser(userParam.FirstName, userParam.LastName, userParam.Email, userParam.Password, userParam.Role);
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
