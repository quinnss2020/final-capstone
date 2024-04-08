using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        IList<User> GetUsers();
        User GetUserById(int id);
        User GetUserByEmail(string email);
        User CreateUser(RegisterUser user);
        //User CreateUser(string fn, string ln, string email, string password, string role, string code);

    }
}
