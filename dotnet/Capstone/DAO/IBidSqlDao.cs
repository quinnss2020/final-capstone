﻿using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IBidSqlDao
    {
        public Bid CreateBid(Bid bid);
        public Bid GetBidById(int id);
    }
}
