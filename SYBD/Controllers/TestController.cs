using Microsoft.AspNetCore.Mvc;
using SYBD.Db;
using SYBD.Db.MongoDb;
using SYBD.Storages.Interfaces;
using SYBD.ViewModels;
using System.Threading.Tasks;

namespace SYBD.Controllers
{
    public class TestController : Controller
    {
        private readonly IPhotographerStorage _photographerStorage;

        private readonly DbService dbMongo;

        private readonly RedisService dbRedis;

        public TestController(IPhotographerStorage photographerStorage, RedisService dbRedis, DbService dbMongo)
        {
            _photographerStorage = photographerStorage;
            this.dbRedis = dbRedis;
            this.dbMongo = dbMongo;
            Program.IsFresh = false;
        }

        public IActionResult Photographer()
        {
            var model = _photographerStorage.PhotographerTest();
            return View(model);
        }

        public IActionResult PhotographerPhoto()
        {
            var model = _photographerStorage.PhotographerPhotoTest();
            return View(model);
        }

        public IActionResult PhotographerPhotoPeriod()
        {
            var model = _photographerStorage.PhotographerPhotoPeriodTest();
            return View(model);
        }

        public async Task<IActionResult> TestGetPhotographers()
        {
            string request = "SELECT * FROM photographers";
            //Redis
            dbRedis.InserData();
            var redisTime = (await dbRedis.GetData()).Item2;
            //Mongo
            var mongoTime = (await dbMongo.GetPhotographers()).Item2;
            return View(new TestPhotographersViewModel
            {
                RequestText = request,
                TimeRedis = redisTime,
                TimeMongoDb = mongoTime
            });
        }
    }
}
