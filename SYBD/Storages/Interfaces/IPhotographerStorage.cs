using SYBD.Db.Models;
using System.Collections.Generic;

namespace SYBD.Storages.Interfaces
{
    public interface IPhotographerStorage
    {
        List<Photographer> GetFullList();
        Photographer GetElement(int id);
        void Insert(Photographer model);
        void Update(Photographer model);
        void Delete(int id);
    }
}
