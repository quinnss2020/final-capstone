using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Capstone.DAO
{
    public class TempUserSqlDao : ITempUserDao
    {
        private readonly string connectionString;

        public TempUserSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public RegisterUser CreateUser(string fn, string ln, string email, string password, string role, string code)
        {
            RegisterUser newUser = null;

            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);


            string sql = "INSERT INTO temp_users (first_name, last_name, email, password_hash, salt, user_role, confirm_code) " +
                         "OUTPUT INSERTED.temp_id " +
                         "VALUES (@fn, @ln, @email, @password_hash, @salt, @user_role, @code);";

            int newUserId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@fn", fn);
                    cmd.Parameters.AddWithValue("@ln", ln);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@user_role", role);
                    cmd.Parameters.AddWithValue("@code", code);

                    newUserId = Convert.ToInt32(cmd.ExecuteScalar());
                    
                }
                newUser = GetTempUserById(newUserId);

            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return newUser;
        }

        public RegisterUser GetTempUserById(int id)
        {
            RegisterUser user = null;

            string sql = "SELECT temp_id, first_name, last_name, email, password_hash, salt, user_role, code FROM temp_users WHERE temp_id = @id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@temp_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        user = MapRowToTempUser(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return user;
        }

        private User MapRowToUser(SqlDataReader reader)
        {
            User user = new User();
            user.Id = Convert.ToInt32(reader["temp_id"]);
            user.FirstName = Convert.ToString(reader["first_name"]);
            user.LastName = Convert.ToString(reader["last_name"]);
            user.Email = Convert.ToString(reader["email"]);
            user.Role = Convert.ToString(reader["user_role"]);
            return user;
        }

        private RegisterUser MapRowToTempUser(SqlDataReader reader)
        {
            RegisterUser user = new RegisterUser();
            user.Id = Convert.ToInt32(reader["temp_id"]);
            user.FirstName = Convert.ToString(reader["first_name"]);
            user.LastName = Convert.ToString(reader["last_name"]);
            user.Email = Convert.ToString(reader["email"]);
            user.Role = Convert.ToString(reader["user_role"]);
            user.Code = Convert.ToString(reader["code"]);
            return user;
        }
    }
}
