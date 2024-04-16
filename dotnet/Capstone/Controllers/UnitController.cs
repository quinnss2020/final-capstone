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
    public class UnitController : ControllerBase
    {
        private readonly IUnitSqlDao unitDao;
        private readonly IUserDao userDao;

        public UnitController(IUnitSqlDao unitDao, IUserDao userDao)
        {
            this.unitDao = unitDao;
            this.userDao = userDao;
        }

        [HttpGet("/units")]
        public ActionResult<List<Unit>> GetAllActiveUnits()
        {
            const string ErrorMessage = "An error has occurred and a list of units was not created.";
            IList<Unit> units = new List<Unit>();

            try
            {
                units = unitDao.GetAllActiveUnits();

                return Ok(units);
            }
            catch (DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }
        }

        //FIX FIX FIX FIX FIX FIX 
        [HttpGet("/units/inactive")]
        public ActionResult<List<Unit>> GetAllInactiveUnits()
        {
            const string ErrorMessage = "An error has occurred and a list of units was not created.";
            IList<Unit> units = new List<Unit>();

            try
            {
                units = unitDao.GetAllActiveUnits();


                foreach(Unit unit in units)
                {
                    return null;
                }
                //for(int i = 0; i < units.Count; i++)
                //{
                //    //if (!unit.Active && !emailSent && user.highestBidder != 1 && user.highestBidder != 2)
                //    //{
                //    //    EmailUtility emailUtility = new EmailUtility

                //    //}
                //}
                return Ok(units);
            }
            catch (DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }

        }

        [HttpGet("/units/{id}")]
        public ActionResult<Unit> GetUnitById(int id)
        {
            Unit unit = new Unit();

            const string ErrorMessage = "An error has occurred and unit was not retrieved.";

            try
            {
                unit = unitDao.GetUnitById(id);
                return Ok(unit);
            }
            catch (DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }
        }

        [HttpPut("/units/checkout")]
        private ActionResult<Unit> UnitCheckout(int unitId)
        {
            Unit unit = unitDao.GetUnitById(unitId);
            if(unit.Active)
            {
                unitDao.UpdateUnit(unit);
                Email email = new Email();
                EmailUtility emailUtility = new EmailUtility();
                User user = userDao.GetUserById(unit.HighestBidder);
                string code = emailUtility.OrderNumberGenerator();
                unit.OrderNumber = code;
                unitDao.UpdateUnit(unit);
                if(unit != null && unit.HighestBidder != 1 && unit.HighestBidder != 2)
                {
                    emailUtility.SendCheckoutEmail(user.Email, unit, code);
                }

            }

            return Ok(unit);

        }
    }
}
