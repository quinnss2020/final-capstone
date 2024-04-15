using Capstone.Exceptions;
using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using Capstone.Controllers;
using Org.BouncyCastle.Asn1.Mozilla;

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

            string sql = "BEGIN TRANSACTION " + "INSERT INTO bids(unit_id, bidder_id, amount) " + "OUTPUT INSERTED.id VALUES(@unitId, @bidderId, @amount) COMMIT";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@unitId", newBid.UnitId);
                    cmd.Parameters.AddWithValue("@bidderId", newBid.BidderId);
                    cmd.Parameters.AddWithValue("@amount", newBid.Amount);
                    //cmd.Parameters.AddWithValue("datePlaced", newBid.DatePlaced);

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

        public IList<Bid> GetAllBidsByUserId(int id)
        {
            IList<Bid> usersBids = new List<Bid>();
            

            string sql = "SELECT id, unit_id, bidder_id, amount, date_placed FROM bids WHERE bidder_id = @id ORDER BY id DESC;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Bid bid = MapRowToBid(reader);
                        usersBids.Add(bid);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return usersBids;
        }

        public IList<Bid> GetBidsByUnitId(int id)
        {
            IList<Bid> unitBids = new List<Bid>();

            string sql = "SELECT  id, unit_id, bidder_id, amount, date_placed FROM bids WHERE unit_id = @unitId ORDER BY date_placed DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@unitId", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Bid bid = MapRowToBid(reader);
                        unitBids.Add(bid);

                    }
                }

            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return unitBids;
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

