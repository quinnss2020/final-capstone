using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserDao userDao;
        public BidController(IBidSqlDao bidDao, IUnitSqlDao unitSqlDao, IUserDao userDao)
        {
            this.bidDao = bidDao;
            this.unitDao = unitSqlDao;
            this.userDao = userDao;

        }

        [Authorize]
        [HttpPost("/bid")]
        public ActionResult<Bid> CreateBid(Bid bid)
        {
            const string ErrorMessage = "An error occurred and bid was not placed.";

            Bid newBid = new Bid();

            Unit unit = unitDao.GetUnitById(bid.UnitId);

            if(bid.Amount > unit.HighestBid && unit.Active)
            {
                try
                {
                    unitDao.UpdateUnit(unit, bid.Amount, bid.BidderId);
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

        [HttpGet("/bids")]
        public ActionResult<List<Bid>> GetAllBidsByUserId()
        {
            const string ErrorMessage = "An error has occurred and a list of ubids was not created.";

            IList<Bid> userBids = new List<Bid>();

            try
            {
                string userEmail = User.Identity.Name;
                User user = userDao.GetUserByEmail(userEmail);
                int userId = user.Id;

                userBids = bidDao.GetAllBidsByUserId(userId);

                return Ok(userBids);
            }
            catch (DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }


        }


    }
}
