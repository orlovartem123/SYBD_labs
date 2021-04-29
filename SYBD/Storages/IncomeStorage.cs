using SYBD.Db;
using SYBD.Db.Models;
using SYBD.Storages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SYBD.Storages
{
    public class IncomeStorage : IIncomeStorage
    {
        public void Delete(int id)
        {
            using (var context = new PhotoGalleryContext())
            {
                Income element = context.Income.FirstOrDefault(rec => rec.Id == id);
                if (element != null)
                {
                    context.Income.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Income not found");
                }
            }
        }

        public Income GetElement(int id)
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Income.FirstOrDefault(rec => rec.Id == id) ?? throw new Exception("Income not found");
            }
        }

        public List<Income> GetFilteredList(Income model)
        {
            throw new NotImplementedException();
        }

        public List<Income> GetFullList()
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Income.ToList();
            }
        }

        public void Insert(Income model)
        {
            using (var context = new PhotoGalleryContext())
            {
                context.Income.Add(model);
                context.SaveChanges();
            }
        }
    }
}
