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

        public (string def, string index) StartTest()
        {
            string def = "";
            string index = "";
            Random rnd = new Random();
            using (var context = new PhotoGalleryContext())
            {
                for (int i = 0; i < 500; i++)
                {
                    var model = new Photographer();
                    model.Name = "Test name" + i;
                    model.Age = rnd.Next(600);
                    model.Status = "TestStatus_" + i;
                    Insert(model);
                }

                //default
                var startTime = System.Diagnostics.Stopwatch.StartNew();
                var testResult = context.Photographer.Where(rec => rec.Age > 325 && rec.Age <= 578);
                startTime.Stop();
                def = startTime.Elapsed.ToString();

                //index
                context.Photographer.FromSqlRaw("CREATE UNIQUE INDEX index_test ON photographer(name, age)");
                startTime = System.Diagnostics.Stopwatch.StartNew();
                var newResult = context.Photographer.Where(rec => rec.Age > 325 && rec.Age <= 578);
                startTime.Stop();
                index = startTime.Elapsed.ToString();
                context.Photographer.RemoveRange(context.Photographer.Where(rec => rec.Name.Contains("Test")));
                context.Photographer.FromSqlRaw("DROP INDEX index_test");
                context.SaveChanges();
            }
            return (def, index);
        }
    }
}


