﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;


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

            string sql = "SELECT id, first_name, last_name, email, password_hash, salt, user_role, confirmed, code, agreed FROM users";

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

            string sql = "SELECT id, first_name, last_name, email, password_hash, salt, user_role, confirmed, code, agreed FROM users WHERE id = @id";

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

            string sql = "SELECT id, first_name, last_name, email, password_hash, salt, user_role, code, confirmed, agreed FROM users WHERE email = @email";

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
            EmailUtility emailUtility = new EmailUtility();

            user.Code = emailUtility.VerificationCodeGenerator();

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
                Email email = emailUtility.FormEmail(user.Email, "Verification Code", "Your account verification code is " + user.Code);
                if (!emailUtility.SendVerificationEmail(email))
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

       public User UpdateAgreed(User user)
        {
            string sql = "UPDATE users SET agreed=@agreed WHERE id =@id;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@agreed", user.Agreed);
                    cmd.Parameters.AddWithValue("@id", user.Id);

                    int count = cmd.ExecuteNonQuery();

                    if (count == 1)
                    {
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
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
            user.Agreed = Convert.ToBoolean(reader["agreed"]);
            
            return user;
        }
    }
}
