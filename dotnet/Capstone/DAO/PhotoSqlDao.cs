using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Capstone.DAO
{
    public class PhotoSqlDao : IPhotoSqlDao
    {
        private readonly string connectionString;

        public PhotoSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public Photo CreatePhoto(Photo photo)
        {
            string sql = "INSERT INTO unit_photos(unit_id, img_URL) OUTPUT INSERTED.id " + "VALUES(@unitId, @imgUrl);";

            int newPhotoId = 0;
            Photo newPhoto = new Photo();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@unitId", photo.UnitId);
                    cmd.Parameters.AddWithValue("@imgUrl", photo.Url);

                    newPhotoId = Convert.ToInt32(cmd.ExecuteScalar());

                    newPhoto = GetPhotoByPhotoId(newPhotoId);
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return newPhoto;
        }

        public Photo GetPhotoByPhotoId(int photoId)
        {
            string sql = "SELECT id, unit_id, img_URL FROM unit_photos WHERE id = @photoId";

            Photo photo = new Photo();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@photoId", photoId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        photo = MapRowToPhoto(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return photo;
        }
        public IList<Photo> GetPhotosByUnitId(int unitId)
        {
            string sql = "SELECT id, unit_id, img_URL FROM unit_photos WHERE unit_id = @unitId";

            IList<Photo> photos = new List<Photo>();
            Photo photo = new Photo();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@unitId", unitId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        photo = MapRowToPhoto(reader);
                        photos.Add(photo);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return photos;
        }
        public Photo DeletePhotoByPhotoId(int photoId)
        {
            Photo photo = new Photo();

            string sql = "DELETE FROM unit_photos WHERE id = @photoId";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@photoId", photoId);

                    cmd.ExecuteNonQuery();
                    photo = GetPhotoByPhotoId(photoId);
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return photo;
        }
        public IList<Photo> DeletePhotosByUnitId(int unitId)
        {
            string sql = "DELETE FROM unit_photos WHERE unit_id = @unitId";

            IList<Photo> photos = new List<Photo>();
            Photo photo = new Photo();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@unitId", unitId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        photo = MapRowToPhoto(reader);
                        photos.Add(photo);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return photos;
        }

        private Photo MapRowToPhoto(SqlDataReader reader)
        {
            Photo photo = new Photo();
            photo.Id = Convert.ToInt32(reader["id"]);
            photo.UnitId = Convert.ToInt32(reader["unit_id"]);
            photo.Url = Convert.ToString(reader["img_URL"]);

            return photo;
        }
    }
}
