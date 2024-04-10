using Org.BouncyCastle.Asn1.Misc;

namespace Capstone.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        public bool Confirmed { get; set; }
        public string Code { get; set; }
        public bool Agreed { get; set; }

        public User(int id, string firstName, string lastName, string email, string passwordHash, 
            string salt, string role, string code, bool confirmed, bool agreed)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            Salt = salt;
            Role = role;
            Confirmed = confirmed;
            Code = code;
            Agreed = agreed;
        }

        public User() { }

    }

    /// <summary>
    /// Model of user data to return upon successful login
    /// </summary>
    public class ReturnUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    /// <summary>
    /// Model to return upon successful login (user data + token)
    /// </summary>
    public class LoginResponse
    {
        public ReturnUser User { get; set; }
        public string Token { get; set; }
    }

    public class AgreedUser
    {
        public LoginUser User { get; set; }
        public bool Agreed { get; set; }
    }

    public class ConfirmUser
    {
        public LoginUser User { get; set; }
        public string Code { get; set; }
    }


    /// <summary>
    /// Model to accept login parameters
    /// </summary>
    public class LoginUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// Model to accept registration parameters
    /// </summary>
    public class RegisterUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
        public string Code { get; set; }
    }


}
