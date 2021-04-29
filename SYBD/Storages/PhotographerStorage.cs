using SYBD.Db;
using SYBD.Db.Models;
using SYBD.Storages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SYBD.Storages
{
    public class PhotographerStorage : IPhotographerStorage
    {
        public void Delete(int id)
        {
            using (var context = new PhotoGalleryContext())
            {
                Photographer element = context.Photographer.FirstOrDefault(rec => rec.Id == id);
                if (element != null)
                {
                    context.Photographer.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Photographer not found");
                }
            }
        }

        public Photographer GetElement(int id)
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Photographer.FirstOrDefault(rec => rec.Id == id) ?? throw new Exception("Photographer not found");
            }
        }

        public List<Photographer> GetFullList()
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Photographer.ToList();
            }
        }

        public void Insert(Photographer model)
        {
            using (var context = new PhotoGalleryContext())
            {
                context.Photographer.Add(model);
                context.SaveChanges();
            }
        }

        public void Update(Photographer model)
        {
            using (var context = new PhotoGalleryContext())
            {
                var element = context.Photographer.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    element.Name = model.Name;
                    element.Age = model.Age;
                    element.Status = model.Status;
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
