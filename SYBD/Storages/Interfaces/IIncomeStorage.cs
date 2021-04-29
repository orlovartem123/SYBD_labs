using SYBD.Db.Models;
using System.Collections.Generic;

namespace SYBD.Storages.Interfaces
{
    public interface IIncomeStorage
    {
        List<Income> GetFullList();
        Income GetElement(int id);
        void Insert(Income model);
        void Delete(int id);
    }
}
