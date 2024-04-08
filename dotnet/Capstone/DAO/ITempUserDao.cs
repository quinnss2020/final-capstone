using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ITempUserDao
    {
        public User GetTempUserById(int id);
        User CreateUser(string fn, string ln, string email, string password, string role);
    }
}
