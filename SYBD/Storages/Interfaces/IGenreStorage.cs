using SYBD.Db.Models;
using System.Collections.Generic;

namespace SYBD.Storages.Interfaces
{
    public interface IGenreStorage
    {
        List<Genre> GetFullList();
        Genre GetElement(int id);
        void Insert(Genre model);
        void Update(Genre model);
        void Delete(int id);
    }
}
