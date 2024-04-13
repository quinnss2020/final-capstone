using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
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
                "order_number, city, size, active, expiration, created, details FROM units";

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
                        units.Add(unit);
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return units;
        }

        public Unit UpdateUnit(Unit unit, int amount, int userId)
        {
            string sql = "UPDATE units SET amount = @amount, highest_bidder = @highest_bidder WHERE id = @unitId";

            Unit newUnit = new Unit();
            int newUnitId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@highest_bidder", userId);
                    cmd.Parameters.AddWithValue("@unitId", unit.Id);

                    newUnitId = unit.Id;

                    newUnit = GetUnitById(newUnitId);
                }
                return newUnit;
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
