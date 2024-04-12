using Capstone.Exceptions;
using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Capstone.DAO
{
    public class BidSqlDao : IBidSqlDao
    {
        private readonly string connectionString;

        public BidSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Bid CreateBid(Bid newBid)
        {
            Bid bid = new Bid();
            int newBidId = 0;

            string sql = "BEGIN TRANSACTION " + "INSERT INTO bids(unit_id, bidder_id, amount, date_placed) " + "OUTPUT INSERTED.id VALUES(@unitId, @bidderId, @amount, @datePlaced) COMMIT";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@unitId", newBid.UnitId);
                    cmd.Parameters.AddWithValue("@bidderId", newBid.BidderId);
                    cmd.Parameters.AddWithValue("@amount", newBid.Amount);
                    cmd.Parameters.AddWithValue("datePlaced", newBid.DatePlaced);

                    newBidId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                bid = GetBidById(newBidId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return bid;
        }

        public Bid GetBidById(int id)
        {
            Bid bid = new Bid();

            string sql = "SELECT id, unit_id, bidder_id, amount, date_placed FROM bids WHERE id = @id";

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
                        bid = MapRowToBid(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return bid;
        }

        private Bid MapRowToBid(SqlDataReader reader)
        {
            Bid bid = new Bid();
            bid.Id = Convert.ToInt32(reader["id"]);
            bid.UnitId = Convert.ToInt32(reader["unit_id"]);
            bid.BidderId = Convert.ToInt32(reader["bidder_id"]);
            bid.Amount = Convert.ToInt32(reader["amount"]);
            bid.DatePlaced = Convert.ToDateTime(reader["date_placed"]);

            return bid;
        }
    }
}

