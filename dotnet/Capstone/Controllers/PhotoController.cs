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
        private readonly IUserDao userDao;

        public PhotoController(IPhotoSqlDao photoDao, IUserDao userDao)
        {
            this.photoDao = photoDao;
            this.userDao = userDao;
        }

        [Authorize]
        [HttpPost("/units/{unitId}/photos/add")]
        public ActionResult<Photo> CreatePhoto(Photo photo)
        {
            const string ErrorMessage = "An error has occurred and the photo was not successfully added to the database";
            User user = userDao.GetUserByEmail(User.Identity.Name);

            if (user.Role == "user")
            {
                return StatusCode(403, "Forbidden");
            }

            try
            {
                Photo newPhoto = photoDao.CreatePhoto(photo);
                return StatusCode(201, newPhoto);
            }
            catch (DaoException)
            {
                return StatusCode(500, ErrorMessage);
            }
        }

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

        [Authorize]
        [HttpDelete("/units/{unitId}/photos/{photoId}/delete")]
        public ActionResult<Photo> DeletePhotoByPhotoId(int photoId)
        {
            User user = userDao.GetUserByEmail(User.Identity.Name);

            if (user.Role == "user")
            {
                return StatusCode(403, "Forbidden");
            }

            try
            {
                Photo photo = photoDao.DeletePhotoByPhotoId(photoId);
                if(photo.Id == 0)
                {
                    return Ok(photo);
                }
                else
                {
                    return StatusCode(500, "Photo was not successfully deleted");
                }
            }
            catch (DaoException)
            {
                return StatusCode(500, "An error occurred and the photo was not deleted");
            }
        }

        [Authorize]
        [HttpDelete("/units/{unitId}/photos/delete")]
        public ActionResult<IList<Photo>> DeletePhotoByUnitId(int unitId)
        {
            User user = userDao.GetUserByEmail(User.Identity.Name);

            if (user.Role == "user")
            {
                return StatusCode(403, "Forbidden");
            }

            try
            {
                IList<Photo> photos = photoDao.DeletePhotosByUnitId(unitId);
                return Ok(photos);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An error occurred and the photos were not deleted");
            }
        }
    }
}
