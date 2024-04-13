using MimeKit;
using System;
using System.Data;

namespace Capstone.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public int LocalId { get; set; }
        public int StartBid { get; set; }
        public int HighestBid { get; set; }
        public int HighestBidder { get; set; }
        public string OrderNumber { get; set; }
        public string City { get; set; }
        public string Size { get; set; }
        public bool Active { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Created { get; set; }
        public string Details { get; set; }

        public Unit() { }

        public Unit(int id, int localId, int startBid, int highestBid, int highestBidder, string orderNumber, string city, string size, bool active, DateTime expiration, DateTime created, string details)
        {
            Id = id;
            LocalId = localId;
            StartBid = startBid;
            HighestBid = highestBid;
            HighestBidder = highestBidder;
            OrderNumber = orderNumber;
            City = city;
            Size = size;
            Active = active;
            Expiration = expiration;
            Created = created;
            Details = details;
        }

    }
}
