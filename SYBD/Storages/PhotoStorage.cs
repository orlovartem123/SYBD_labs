using SYBD.Db;
using SYBD.Db.Models;
using SYBD.Storages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SYBD.Storages
{
    public class PhotoStorage : IPhotoStorage
    {
        public void Delete(int id)
        {
            using (var context = new PhotoGalleryContext())
            {
                Photo element = context.Photo.FirstOrDefault(rec => rec.Id == id);
                if (element != null)
                {
                    context.Photo.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Photo not found");
                }
            }
        }

        public Photo GetElement(int id)
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Photo.FirstOrDefault(rec => rec.Id == id) ?? throw new Exception("Photo not found");
            }
        }

        public List<Photo> GetFullList()
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Photo.ToList();
            }
        }

        public void Insert(Photo model)
        {
            using (var context = new PhotoGalleryContext())
            {
                context.Photo.Add(model);
                context.SaveChanges();
            }
        }

        public void Update(Photo model)
        {
            using (var context = new PhotoGalleryContext())
            {
                var element = context.Photo.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    element.Photographerid = model.Photographerid;
                    element.Photodate = model.Photodate;
                    element.Quality = model.Quality;
                    element.Rating = model.Rating;
                    element.Price = model.Price;
                    element.Genreid = model.Genreid;
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
