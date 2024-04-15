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
    public class UnitSqlDao : IUnitSqlDao
    {
        private readonly string connectionString;

        public UnitSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Unit GetUnitById(int id)
        {
            Unit unit = new Unit();
            string sql = "SELECT id, local_id, start_bid, highest_bid, highest_bidder, " +
                "order_number, city, size, active, expiration, created, details FROM units WHERE id = @id";

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
                        unit = MapRowToUnit(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return unit;
        }

        public IList<Unit> GetAllUnits()
        {

            IList<Unit> units = new List<Unit>();

            string sql = "SELECT id, local_id, start_bid, highest_bid, highest_bidder, " +
                "order_number, city, size, active, expiration, created, details FROM units WHERE active = 1";

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Unit unit = MapRowToUnit(reader);

                        if(unit.Expiration > DateTime.Now)
                        {
                            units.Add(unit);
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return units;
        }

        public Unit CreateUnit(Unit unit)
        {
            Unit newUnit = new Unit();
            int newUnitId = 0;

            string sql = "INSERT INTO units(local_id, start_bid, highest_bid, order_number, city, size, active, expiration, details) " +
                "OUTPUT INSERTED.id VALUES(@localId, @startBid, @highestBid, @orderNumber, @city, @size, @active, @expiration, @details);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@localId", newUnit.LocalId);
                    cmd.Parameters.AddWithValue("@startBid", newUnit.StartBid);
                    cmd.Parameters.AddWithValue("@highestBid", newUnit.HighestBid);
                    cmd.Parameters.AddWithValue("@orderNumber", newUnit.OrderNumber);
                    cmd.Parameters.AddWithValue("@city", newUnit.City);
                    cmd.Parameters.AddWithValue("@size", newUnit.Size);
                    cmd.Parameters.AddWithValue("@active", newUnit.Active);
                    cmd.Parameters.AddWithValue("@expiration", newUnit.Expiration);
                    cmd.Parameters.AddWithValue("@details", newUnit.Details);

                    newUnitId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                newUnit = GetUnitById(newUnitId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return newUnit;
        }

        public Unit UpdateUnit(Unit unit)
        {
            string sql = "UPDATE units SET local_id = @localId, start_bid = @startBid, highest_bid = @highestBid, " +
                "highest_bidder = @highestBidder, order_number = @orderNumber, city = @city, size = @size, active = @active, " +
                "expiration = @expiration, details = @details WHERE id = @unitId";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@localId", unit.LocalId);
                    cmd.Parameters.AddWithValue("@startBid", unit.StartBid);
                    cmd.Parameters.AddWithValue("@highestBid", unit.HighestBid);
                    cmd.Parameters.AddWithValue("@highestBidder", unit.HighestBidder);
                    cmd.Parameters.AddWithValue("@orderNumber", unit.OrderNumber);
                    cmd.Parameters.AddWithValue("@city", unit.City);
                    cmd.Parameters.AddWithValue("@size", unit.Size);
                    cmd.Parameters.AddWithValue("@active", unit.Active);
                    cmd.Parameters.AddWithValue("@expiration", unit.Expiration);
                    cmd.Parameters.AddWithValue("@details", unit.Details);
                    cmd.Parameters.AddWithValue("@unitId", unit.Id);
                    int count = cmd.ExecuteNonQuery();

                    if(count == 1)
                    {
                        return unit;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
        }

        public Unit DeleteUnit(int unitId)
        {
            string sql = "DELETE FROM units WHERE id=@unitId";
            Unit unit = new Unit();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@unitId", unitId);

                    cmd.ExecuteNonQuery();

                    unit = GetUnitById(unitId);
                }
                return unit;
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
        }
        private Unit MapRowToUnit(SqlDataReader reader)
        {
            Unit unit = new Unit();
            unit.Id = Convert.ToInt32(reader["id"]);
            unit.LocalId = Convert.ToInt32(reader["local_id"]);
            unit.StartBid = Convert.ToInt32(reader["start_bid"]);
            unit.HighestBid = Convert.ToInt32(reader["highest_bid"]);
            unit.HighestBidder = Convert.ToInt32(reader["highest_bidder"]);
            unit.OrderNumber = Convert.ToString(reader["order_number"]);
            unit.City = Convert.ToString(reader["city"]);
            unit.Size = Convert.ToString(reader["size"]);
            unit.Active = Convert.ToBoolean(reader["active"]);
            unit.Expiration = Convert.ToDateTime(reader["expiration"]);
            unit.Created = Convert.ToDateTime(reader["created"]);
            unit.Details = Convert.ToString(reader["details"]);

            return unit;
        }
    }
}
