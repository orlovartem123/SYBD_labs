using SYBD.Db;
using SYBD.Db.Models;
using SYBD.Storages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SYBD.Storages
{
    public class GenreStorage : IGenreStorage
    {
        public void Delete(int id)
        {
            using (var context = new PhotoGalleryContext())
            {
                Genre element = context.Genre.FirstOrDefault(rec => rec.Id == id);
                if (element != null)
                {
                    context.Genre.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Genre not found");
                }
            }
        }

        public Genre GetElement(int id)
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Genre.FirstOrDefault(rec => rec.Id == id) ?? throw new Exception("Genre not found");
            }
        }

        public List<Genre> GetFullList()
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Genre.ToList();
            }
        }

        public void Insert(Genre model)
        {
            using (var context = new PhotoGalleryContext())
            {
                context.Genre.Add(model);
                context.SaveChanges();
            }
        }

        public void Update(Genre model)
        {
            using (var context = new PhotoGalleryContext())
            {
                var element = context.Genre.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    element.Name = model.Name;
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
