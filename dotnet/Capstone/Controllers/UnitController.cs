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
            const string ErrorMessage = "An error has occurred and a list of expired units was not created.";
            IList<Unit> units = new List<Unit>();

            try
            {
                units = unitDao.GetAllInactiveUnits();
                return Ok(units);
            }
            catch (DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }

        }

        [HttpGet("/units/all")]
        public ActionResult<List<Unit>> GetAllUnits()
        {
            const string ErrorMessage = "An error has occurred and a list of expired units was not created.";
            IList<Unit> units = new List<Unit>();

            try
            {
                units = unitDao.GetAllUnits();
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

        [Authorize]
        [HttpPut("/units/{id}/edit")]
        public ActionResult<Unit> UpdateUnit(Unit unit)
        {
            try
            {
                Unit updatedUnit = new Unit();
                updatedUnit = unitDao.UpdateUnit(unit);
                if(updatedUnit == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(updatedUnit);
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, "Could not update unit");

            }
        }


        [HttpPut("/units/checkout")]
        public ActionResult<List<Unit>> UnitCheckout()
        {
            IList<Unit> units = new List<Unit>();

            units = unitDao.GetAllInactiveUnits();

            foreach (Unit unit in units)
            {
                if (unit.Active)
                {
                    unit.Active = false;
                    unitDao.UpdateUnit(unit);
                    Email email = new Email();
                    EmailUtility emailUtility = new EmailUtility();
                    User user = userDao.GetUserById(unit.HighestBidder);
                    string code = emailUtility.OrderNumberGenerator();
                    unit.OrderNumber = code;
                    unitDao.UpdateUnit(unit);
                    if (unit != null)
                    {
                        emailUtility.SendCheckoutEmail(user.Email, unit, code);
                        unit.EmailSent = true;
                        unitDao.UpdateUnit(unit);
                    }

                }


            }
            return Ok(units);

        }
    }
}
