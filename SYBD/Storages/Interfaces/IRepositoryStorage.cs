using SYBD.Db.Models;
using System.Collections.Generic;

namespace SYBD.Storages.Interfaces
{
    public interface IRepositoryStorage
    {
        List<Repository> GetFullList();
    }
}
