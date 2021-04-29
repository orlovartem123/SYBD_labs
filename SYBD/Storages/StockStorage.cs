using SYBD.Db;
using SYBD.Db.Models;
using SYBD.Storages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SYBD.Storages
{
    public class StockStorage : IStockStorage
    {
        public void Delete(int id)
        {
            using (var context = new PhotoGalleryContext())
            {
                Stock element = context.Stock.FirstOrDefault(rec => rec.Id == id);
                if (element != null)
                {
                    context.Stock.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Stock not found");
                }
            }
        }

        public Stock GetElement(int id)
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Stock.FirstOrDefault(rec => rec.Id == id) ?? throw new Exception("Stock not found");
            }
        }

        public List<Stock> GetFullList()
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Stock.ToList();
            }
        }

        public void Insert(Stock model)
        {
            using (var context = new PhotoGalleryContext())
            {
                context.Stock.Add(model);
                context.SaveChanges();
            }
        }

        public void Update(Stock model)
        {
            using (var context = new PhotoGalleryContext())
            {
                var element = context.Stock.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    element.Address = model.Address;
                    context.SaveChanges();
                }
                else
                {
                    Insert(model);
                }
            }
        }
    }
}
