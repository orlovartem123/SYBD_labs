using SYBD.Db.Models;
using System.Collections.Generic;

namespace SYBD.Storages.Interfaces
{
    public interface ISaleStorage
    {
        List<Sale> GetFullList();
        Sale GetElement(int id);
        void Insert(Sale model);
        void Delete(int id);
    }
}
