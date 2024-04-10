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

    }
}
