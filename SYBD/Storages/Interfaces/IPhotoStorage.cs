using SYBD.Db.Models;
using System.Collections.Generic;

namespace SYBD.Storages.Interfaces
{
    public interface IPhotoStorage
    {
        List<Photo> GetFullList();
        Photo GetElement(int id);
        void Insert(Photo model);
        void Update(Photo model);
        void Delete(int id);
    }
}
