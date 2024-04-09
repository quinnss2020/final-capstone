using System;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using System.Net;
using MailKit;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;

namespace Capstone.DAO
{
    public class UserSqlDao : IUserDao
    {
        private readonly string connectionString;


        public UserSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<User> GetUsers()
        {
            IList<User> users = new List<User>();

            string sql = "SELECT id, first_name, last_name, email, password_hash, salt, user_role, confirmed, code FROM users";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = MapRowToUser(reader);
                        users.Add(user);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return users;
        }

        public User GetUserById(int id)
        {
            User user = null;

            string sql = "SELECT id, first_name, last_name, email, password_hash, salt, user_role, confirmed, code FROM users WHERE id = @id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) 
                    {
                        user = MapRowToUser(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return user;
        }

        public User GetUserByEmail(string email)
        {
            User user = null;

            string sql = "SELECT id, first_name, last_name, email, password_hash, salt, user_role, confirmed, code FROM users WHERE email = @email";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = MapRowToUser(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return user;
        }

        public User CreateUser(RegisterUser user)
        {
            User newUser = null;

            user.Code = VerificationCodeGenerator();

            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(user.Password);

            string sql = "INSERT INTO users (first_name, last_name, email, password_hash, salt, user_role, code) " +
                         "OUTPUT INSERTED.id " +
                         "VALUES (@fn, @ln, @email, @password_hash, @salt, @user_role, @code)";

            int newUserId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@fn", user.FirstName);
                    cmd.Parameters.AddWithValue("@ln", user.LastName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@user_role", user.Role);
                    cmd.Parameters.AddWithValue("@code", user.Code);

                    newUserId = Convert.ToInt32(cmd.ExecuteScalar());
                    
                }
                newUser = GetUserById(newUserId);
                if (!SendVerificationEmail(newUser))
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return newUser;
        }

        public User UpdateUser(User updatedUser)
        {
            string sql = "UPDATE users SET confirmed=@confirmed WHERE id =@id;";
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using(SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@confirmed", updatedUser.Confirmed);
                    cmd.Parameters.AddWithValue("@id", updatedUser.Id);

                    int count = cmd.ExecuteNonQuery();

                    if(count == 1)
                    {
                        return updatedUser;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            
        }

        //public LoginResponse CreateToken(ReturnUser user)
        //{
        //    string token = tokenGenerator.GenerateToken(user.Id, user.Email, user.Role);

        //    // Create a ReturnUser object to return to the client
        //    LoginResponse retUser = new LoginResponse() { User = new ReturnUser() { Id = user.Id, Email = user.Email, Role = user.Role }, Token = token };
        //    return retUser;

        //}

        private bool SendVerificationEmail(User user)
        {
            try
            {

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("deltastoragesolutions@outlook.com"));
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = $"Use this code to verify your account: {user.Code}";
                email.Body = new TextPart(TextFormat.Html) { Text = "<h1>Potential Html</h1>" };

                //send email

                using var smtp = new SmtpClient();
                smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
                smtp.AuthenticationMechanisms.Remove("XOAUTH2");
                smtp.Authenticate("deltastoragesolutions@outlook.com", "JakeQuinnSerinaTiana");
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch(SmtpCommandException ex)
            {
                Console.WriteLine("Email failed: " + ex);
                return false;
            }
        }

        private string VerificationCodeGenerator()
        {
            Random rand = new Random();

            return rand.Next(0, 1000000).ToString("000000");
        }
        private User MapRowToUser(SqlDataReader reader)
        {
            User user = new User();
            user.Id = Convert.ToInt32(reader["id"]);
            user.FirstName = Convert.ToString(reader["first_name"]);
            user.LastName = Convert.ToString(reader["last_name"]);
            user.Email = Convert.ToString(reader["email"]);
            user.PasswordHash = Convert.ToString(reader["password_hash"]);
            user.Salt = Convert.ToString(reader["salt"]);
            user.Role = Convert.ToString(reader["user_role"]);
            user.Confirmed = Convert.ToBoolean(reader["confirmed"]);
            user.Code = Convert.ToString(reader["code"]);
            
            return user;
        }
    }
}
