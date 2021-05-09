using Microsoft.EntityFrameworkCore;
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

        public (string def, string index) PhotographerTest()
        {
            string def = "";
            string index = "";
            Random rnd = new Random();
            using (var context = new PhotoGalleryContext())
            {
                for (int i = 0; i < 5000; i++)
                {
                    var model = new Photographer();
                    model.Name = "Test name" + i;
                    model.Age = rnd.Next(600);
                    model.Status = "TestStatus_" + i;
                    Insert(model);
                }

                //default
                var startTime = System.Diagnostics.Stopwatch.StartNew();
                var testResult = context.Photographer.FromSqlRaw("SELECT name, age, status FROM photographer");
                startTime.Stop();
                def = startTime.Elapsed.ToString();

                //index
                context.Photographer.FromSqlRaw("CREATE UNIQUE INDEX index_test ON photographer(name, age, status)");
                startTime = System.Diagnostics.Stopwatch.StartNew();
                var newResult = context.Photographer.FromSqlRaw("SELECT name, age, status FROM photographer");
                startTime.Stop();
                index = startTime.Elapsed.ToString();
                context.Photographer.RemoveRange(context.Photographer.Where(rec => rec.Name.Contains("Test")));
                context.Photographer.FromSqlRaw("DROP INDEX index_test");
                context.SaveChanges();
            }
            return (def, index);
        }

        public (string def, string index) PhotographerPhotoTest()
        {
            string def = "";
            string index = "";
            Random rnd = new Random();
            using (var context = new PhotoGalleryContext())
            {
                for (int i = 0; i < 5000; i++)
                {
                    var model = new Photographer();
                    model.Name = "Test name" + i;
                    model.Age = rnd.Next(600);
                    model.Status = "TestStatus_" + i;
                    Insert(model);
                }

                for (int i = 0; i < 5000; i++)
                {
                    string date = rnd.Next(2005, 2035).ToString() + '-' + rnd.Next(1, 12) + '-' + rnd.Next(1, 28) + ' ' + rnd.Next(0, 23) + ':' + rnd.Next(0, 59);
                    var model = new Photo();
                    model.Photographerid = context.Photographer.Skip(rnd.Next(0, context.Photographer.Count())).FirstOrDefault()?.Id;
                    model.Photodate = DateTime.Parse(date);
                    model.Quality = "Test quality " + i;
                    model.Rating = rnd.Next(200);
                    model.Price = (decimal)rnd.NextDouble();
                    context.Photo.Add(model);
                }

                //default
                var startTime = System.Diagnostics.Stopwatch.StartNew();
                var testResult = context.Photographer.FromSqlRaw("SELECT m.name, m.age, m.status, p.rating, p.photodate, p.price FROM photographer m JOIN photo p ON m.id = p.photographerid");
                startTime.Stop();
                def = startTime.Elapsed.ToString();

                //index
                context.Photographer.FromSqlRaw("CREATE UNIQUE INDEX index_test ON photographer(name, age, status); CREATE UNIQUE INDEX index_test2 ON photo(rating, photodate, price)");
                startTime = System.Diagnostics.Stopwatch.StartNew();
                var newResult = context.Photographer.FromSqlRaw("SELECT m.name, m.age, m.status, p.rating, p.photodate, p.price FROM photographer m JOIN photo p ON m.id = p.photographerid");
                startTime.Stop();
                index = startTime.Elapsed.ToString();
                context.Photographer.RemoveRange(context.Photographer.Where(rec => rec.Name.Contains("Test")));
                context.Photo.RemoveRange(context.Photo.Where(rec => rec.Quality.Contains("Test")));
                context.Photographer.FromSqlRaw("DROP INDEX index_test; DROP INDEX index_test2");
                context.SaveChanges();
            }
            return (def, index);
        }

        public (string def, string index) PhotographerPhotoPeriodTest()
        {
            string def = "";
            string index = "";
            Random rnd = new Random();
            using (var context = new PhotoGalleryContext())
            {
                for (int i = 0; i < 5000; i++)
                {
                    var model = new Photographer();
                    model.Name = "Test name" + i;
                    model.Age = rnd.Next(600);
                    model.Status = "TestStatus_" + i;
                    Insert(model);
                }

                for (int i = 0; i < 5000; i++)
                {
                    string date = rnd.Next(2005, 2035).ToString() + '-' + rnd.Next(1, 12) + '-' + rnd.Next(1, 28) + ' ' + rnd.Next(0, 23) + ':' + rnd.Next(0, 59);
                    var model = new Photo();
                    model.Photographerid = context.Photographer.Skip(rnd.Next(0, context.Photographer.Count())).FirstOrDefault()?.Id;
                    model.Photodate = DateTime.Parse(date);
                    model.Quality = "Test quality " + i;
                    model.Rating = rnd.Next(200);
                    model.Price = (decimal)rnd.NextDouble();
                    context.Photo.Add(model);
                }

                //default
                var startTime = System.Diagnostics.Stopwatch.StartNew();
                var testResult = context.Photographer.FromSqlRaw("select m.name, m.age, m.status, p.rating, p.photodate, p.price from photographer m join photo p on m.id = p.photographerid and p.photodate between '2020-1-1' and '2025-1-1'");
                startTime.Stop();
                def = startTime.Elapsed.ToString();

                //index
                context.Photographer.FromSqlRaw("CREATE UNIQUE INDEX index_test ON photographer(name, age, status); CREATE UNIQUE INDEX index_test2 ON photo(rating, photodate, price)");
                startTime = System.Diagnostics.Stopwatch.StartNew();
                var newResult = context.Photographer.FromSqlRaw("SELECT m.name, m.age, m.status, p.rating, p.photodate, p.price FROM photographer m JOIN photo p ON m.id = p.photographerid");
                startTime.Stop();
                index = startTime.Elapsed.ToString();
                context.Photographer.RemoveRange(context.Photographer.Where(rec => rec.Name.Contains("Test")));
                context.Photo.RemoveRange(context.Photo.Where(rec => rec.Quality.Contains("Test")));
                context.Photographer.FromSqlRaw("DROP INDEX index_test; DROP INDEX index_test2");
                context.SaveChanges();
            }
            return (def, index);
        }
    }
}


