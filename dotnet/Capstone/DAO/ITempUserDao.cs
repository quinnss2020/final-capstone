using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ITempUserDao
    {
        public RegisterUser GetTempUserById(int id);
        RegisterUser CreateUser(string fn, string ln, string email, string password, string role, string code);
    }
}
