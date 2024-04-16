using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IPhotoSqlDao
    {
        public Photo CreatePhoto(Photo photo);
        public IList<Photo> GetAllPhotosByUnitId(int unitId);
        public Photo GetPhotoByPhotoId(int photoId);
        public Photo DeletePhotoByPhotoID(int photoId);

        public IList<Photo> DeletePhotosByUnitId(int unitId);

    }
}

