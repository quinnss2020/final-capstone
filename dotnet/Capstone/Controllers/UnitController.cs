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

        public UnitController(IUnitSqlDao unitDao)
        {
            this.unitDao = unitDao;
        }

        [HttpGet("/units")]
        public ActionResult<List<Unit>> GetAllUnits()
        {
            const string ErrorMessage = "An error has occurred and a list of units was not created.";
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

    }
}
