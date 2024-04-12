using System;

namespace Capstone.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public int BidderId { get; set; }
        public int Amount { get; set; }
        public DateTime DatePlaced { get; set; }

    }
}
