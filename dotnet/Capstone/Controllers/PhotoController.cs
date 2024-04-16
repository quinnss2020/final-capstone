using Capstone.DAO;
using Capstone.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Microsoft.AspNetCore.Identity;
using Capstone.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoSqlDao photoDao;

        public PhotoController(IPhotoSqlDao photoDao)
        {
            this.photoDao = photoDao;
        }

        [Authorize]
        [HttpPost("/units/{unitId}/photos")]


        [HttpGet("/units/{unitId}/photos")]
        public ActionResult<List<Photo>> GetPhotosByUnitId(int unitId)
        {
            const string ErrorMessage = "An error has occurred and a list of photos cannot be retrieved.";
            IList<Photo> photos = new List<Photo>();

            try
            {
                photos = photoDao.GetPhotosByUnitId(unitId);
                return Ok(photos);
            }
            catch(DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }
        }

        [HttpGet("/units/{unitId}/photos/{photoId}")]
        public ActionResult<Photo> GetPhotoByPhotoId(int photoId)
        {
            const string ErrorMessage = "An error has occurred and the photo cannot be retrieved";
            Photo photo = new Photo();

            try
            {
                photo = photoDao.GetPhotoByPhotoId(photoId);
                return Ok(photo);
            }
            catch
            {
                return StatusCode(500, ErrorMessage);
            }
        }
    }
}
