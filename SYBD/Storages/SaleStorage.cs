using SYBD.Db;
using SYBD.Db.Models;
using SYBD.Storages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SYBD.Storages
{
    public class SaleStorage : ISaleStorage
    {
        public void Delete(int id)
        {
            using (var context = new PhotoGalleryContext())
            {
                Sale element = context.Sale.FirstOrDefault(rec => rec.Id == id);
                if (element != null)
                {
                    context.Sale.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Sale not found");
                }
            }
        }

        public Sale GetElement(int id)
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Sale.FirstOrDefault(rec => rec.Id == id) ?? throw new Exception("Sale not found");
            }
        }

        public List<Sale> GetFullList()
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Sale.ToList();
            }
        }

        public void Insert(Sale model)
        {
            using (var context = new PhotoGalleryContext())
            {
                context.Sale.Add(model);
                context.SaveChanges();
            }
        }
    }
}
