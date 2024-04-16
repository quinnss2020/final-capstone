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
        private readonly IPhotoSqlDao photoDao;

        public UnitController(IUnitSqlDao unitDao, IUserDao userDao, IPhotoSqlDao photoDao)
        {
            this.unitDao = unitDao;
            this.userDao = userDao;
            this.photoDao = photoDao;
        }

        [Authorize]
        [HttpPost("/units/add")]
        public ActionResult<Unit> CreateUnit(Unit unit)
        {
            Unit newUnit = new Unit();

            User user = userDao.GetUserByEmail(User.Identity.Name);

            if (user.Role == "user")
            {
                return StatusCode(403, "Forbidden");
            }

            try
            {
                newUnit = unitDao.CreateUnit(unit);
                if(newUnit != null)
                {
                    return StatusCode(201, "Unit was created");
                }
                else
                {
                    return StatusCode(500, "Unit was not successfully created");
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, "An error occurred and a unit could not be added.");
            }
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
                if (updatedUnit == null)
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
                    if (unit != null && unit.HighestBidder != 1 && unit.HighestBidder != 2)
                    {
                        emailUtility.SendCheckoutEmail(user.Email, unit, code);
                        unit.EmailSent = true;
                        unitDao.UpdateUnit(unit);
                    }
                    

                }


            }
            return Ok(units);

        }

        [Authorize]
        [HttpDelete("/units/{unitId}/delete")]
        public ActionResult<Unit> DeleteUnitByUnitId(int unitId)
        {
            try
            {
                IList<Photo> photos = photoDao.DeletePhotosByUnitId(unitId);
                Unit unit = unitDao.DeleteUnit(unitId);

                if (unit.Id == 0)
                {
                    return Ok(unit);
                }
                else
                {
                    return StatusCode(500, "Unit was not successfully deleted");
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, "An error occurred and the unit was not deleted");
            }
        }
    }
}