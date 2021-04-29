using SYBD.Db.Models;
using System.Collections.Generic;

namespace SYBD.Storages.Interfaces
{
    public interface IStockStorage
    {
        List<Stock> GetFullList();
        Stock GetElement(int id);
        void Insert(Stock model);
        void Update(Stock model);
        void Delete(int id);
    }
}
