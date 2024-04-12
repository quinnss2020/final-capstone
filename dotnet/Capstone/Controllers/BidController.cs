using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Capstone.Controllers
{
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidSqlDao bidDao;
        private readonly IUnitSqlDao unitDao;
        public BidController(IBidSqlDao bidDao, IUnitSqlDao unitSqlDao)
        {
            this.bidDao = bidDao;
            this.unitDao = unitSqlDao;

        }

        [Authorize]
        [HttpPost("/units/{unitId}/bid")]
        public ActionResult<Bid> CreateBid(Bid bid)
        {
            const string ErrorMessage = "An error occurred and bid was not placed.";

            Bid newBid = new Bid();

            Unit unit = unitDao.GetUnitById(bid.UnitId);

            if(bid.Amount > unit.HighestBid)
            {
                try
                {
                    unitDao.UpdateUnit(unit, bid.Amount);
                    newBid = bidDao.CreateBid(bid);
                    return Created($"/account/bids", newBid);
                }
                catch(DaoException)
                {
                    return StatusCode(500, ErrorMessage);
                }
            }
            else
            {
                return StatusCode(400, "Bid amount did not exceed the unit's highest bid");
            }
        }

        [HttpGet("/bids/{id}")]
        public ActionResult<Bid> GetBidById(int id)
        {
            const string ErrorMessage = "An error has occurred and the bid was not retrieved.";

            Bid bid = new Bid();

            try
            {
                bid = bidDao.GetBidById(id);
            }
            catch (DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }

            return Ok(bid);
        }
    }
}
