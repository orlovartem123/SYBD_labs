using SYBD.Db;
using SYBD.Db.Models;
using SYBD.Storages.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SYBD.Storages
{
    public class RepositoryStorage : IRepositoryStorage
    {
        public List<Repository> GetFullList()
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Repository.ToList();
            }
        }
    }
}
