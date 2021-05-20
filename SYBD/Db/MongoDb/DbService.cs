﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SYBD.Db.MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SYBD.Db.MongoDb
{
    public class DbService
    {
        IMongoCollection<Photographer> Photographers;
        IMongoCollection<Genre> Genres;
        IMongoCollection<Income> Incomes;
        IMongoCollection<Sale> Sales;
        IMongoCollection<Photo> Photos;
        IMongoCollection<Stock> Stocks;

        public DbService(IConfiguration conf)
        {
            var connectionString = conf["MongoDb"];
            var connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            Photographers = database.GetCollection<Photographer>("Photographers");
            Genres = database.GetCollection<Genre>("Genres");
            Incomes = database.GetCollection<Income>("Incomes");
            Sales = database.GetCollection<Sale>("Sales");
            Stocks = database.GetCollection<Stock>("Stocks");
            Photos = database.GetCollection<Photo>("Photos");
        }

        #region Photographers

        public async Task<Tuple<IEnumerable<Photographer>, string>> GetPhotographers()
        {
            var builder = new FilterDefinitionBuilder<Photographer>();
            IEnumerable<Photographer> data = null;
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            await Task.Run(() =>
            {
                data = Photographers.Find(builder.Empty).ToList();
                startTime.Stop();
                Console.WriteLine("mongo"+data.Count());
            });
            return Tuple.Create(data, startTime.Elapsed.ToString());
        }

        private void Create(IEnumerable<Photographer> photographers)
        {
            Photographers.InsertManyAsync(photographers);
        }

        private void RemovePhotographers()
        {
            var builder = new FilterDefinitionBuilder<Photographer>();
            Photographers.DeleteMany(builder.Empty);
        }

        private Photographer CreateMongoModel(Db.Models.Photographer pgPhotographer)
        {
            return new Photographer
            {
                Id = pgPhotographer.Id.ToString(),
                Name = pgPhotographer.Name,
                Age = pgPhotographer.Age,
                Status = pgPhotographer.Status
            };
        }

        #endregion

        #region Genres

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            var builder = new FilterDefinitionBuilder<Genre>();
            return await Genres.Find(builder.Empty).ToListAsync();
        }

        private void Create(IEnumerable<Genre> genres)
        {
            Genres.InsertManyAsync(genres);
        }

        private void RemoveGenres()
        {
            var builder = new FilterDefinitionBuilder<Genre>();
            Genres.DeleteMany(builder.Empty);
        }

        private Genre CreateMongoModel(Db.Models.Genre pgGenre)
        {
            return new Genre
            {
                Id = pgGenre.Id.ToString(),
                Name = pgGenre.Name
            };
        }

        #endregion

        #region Photos

        public async Task<IEnumerable<Photo>> GetPhotos()
        {
            var builder = new FilterDefinitionBuilder<Photo>();
            return await Photos.Find(builder.Empty).ToListAsync();
        }

        private void Create(IEnumerable<Photo> photos)
        {
            Photos.InsertManyAsync(photos);
        }

        private void RemovePhotos()
        {
            var builder = new FilterDefinitionBuilder<Photo>();
            Photos.DeleteMany(builder.Empty);
        }

        private Photo CreateMongoModel(Db.Models.Photo pgPhoto)
        {
            return new Photo
            {
                Id = pgPhoto.Id.ToString(),
                PhotographerId = pgPhoto.Photographerid.ToString(),
                GenreId = pgPhoto.Genreid.ToString(),
                PhotoDate = pgPhoto.Photodate,
                Quality = pgPhoto.Quality,
                Rating = pgPhoto.Rating ?? 0,
                Price = pgPhoto.Price
            };
        }

        #endregion

        #region Stocks

        public async Task<IEnumerable<Stock>> GetStocks()
        {
            var builder = new FilterDefinitionBuilder<Stock>();
            return await Stocks.Find(builder.Empty).ToListAsync();
        }

        private void Create(IEnumerable<Stock> stocks)
        {
            Stocks.InsertManyAsync(stocks);
        }

        private void RemoveStocks()
        {
            var builder = new FilterDefinitionBuilder<Stock>();
            Stocks.DeleteMany(builder.Empty);
        }

        private Stock CreateMongoModel(Db.Models.Stock pgStock)
        {
            var result = new Stock
            {
                Id = pgStock.Id.ToString(),
                Address = pgStock.Address
            };
            result.Photos = new List<Tuple<string, int>>();
            result.Photos.AddRange(pgStock.Repository.Select(rec => Tuple.Create(rec.Photoid.ToString(), rec.Count)));
            return result;
        }

        #endregion

        #region Incomes

        public async Task<IEnumerable<Income>> GetIncomes()
        {
            var builder = new FilterDefinitionBuilder<Income>();
            return await Incomes.Find(builder.Empty).ToListAsync();
        }

        private void Create(IEnumerable<Income> incomes)
        {
            Incomes.InsertManyAsync(incomes);
        }

        private void RemoveIncomes()
        {
            var builder = new FilterDefinitionBuilder<Income>();
            Incomes.DeleteMany(builder.Empty);
        }

        private Income CreateMongoModel(Db.Models.Income pgIncome)
        {
            return new Income
            {
                Id = pgIncome.Id.ToString(),
                IncomeDate = pgIncome.Incomedate,
                PhotoId = pgIncome.Photoid.ToString(),
                Count = pgIncome.Count,
                StockId = pgIncome.Stockid.ToString()
            };
        }

        #endregion

        #region Sales

        public async Task<IEnumerable<Sale>> GetSales()
        {
            var builder = new FilterDefinitionBuilder<Sale>();
            return await Sales.Find(builder.Empty).ToListAsync();
        }

        private void Create(IEnumerable<Sale> sales)
        {
            Sales.InsertManyAsync(sales);
        }

        private void RemoveSales()
        {
            var builder = new FilterDefinitionBuilder<Sale>();
            Sales.DeleteMany(builder.Empty);
        }

        private Sale CreateMongoModel(Db.Models.Sale pgSale)
        {
            return new Sale
            {
                Id = pgSale.Id.ToString(),
                SoldDate = pgSale.Solddate,
                PhotoId = pgSale.Photoid.ToString(),
                Count = pgSale.Count,
                StockId = pgSale.Stockid.ToString()
            };
        }

        #endregion

        public void StartTransferData()
        {
            RemoveGenres();
            RemoveIncomes();
            RemovePhotos();
            RemoveSales();
            RemoveStocks();
            RemovePhotographers();
            using (var context = new PhotoGalleryContext())
            {
                Create(context.Photographer.Select(CreateMongoModel));
                Create(context.Genre.Select(CreateMongoModel));
                Create(context.Photo.Select(CreateMongoModel));
                Create(context.Stock.Include(rec => rec.Repository).Select(CreateMongoModel));
                Create(context.Income.Select(CreateMongoModel));
                Create(context.Sale.Select(CreateMongoModel));
            }
            var obj = new RedisService();
        }
    }
}
