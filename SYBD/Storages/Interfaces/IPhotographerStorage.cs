using SYBD.Db.Models;
using System.Collections.Generic;

namespace SYBD.Storages.Interfaces
{
    public interface IPhotographerStorage
    {
        List<Photographer> GetFullList();
        Photographer GetElement(int id);
        (string def, string index) StartTest();
        void Insert(Photographer model);
        void Update(Photographer model);
        void Delete(int id);
    }
}
