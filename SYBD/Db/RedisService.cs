using StackExchange.Redis;
using SYBD.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SYBD.Db
{
    public class RedisService
    {
        private ConnectionMultiplexer redis { get; set; }

        private IDatabase db { get; set; }

        private int index { get; set; }

        public RedisService()
        {
            redis = ConnectionMultiplexer.Connect("localhost");
            db = redis.GetDatabase();
        }

        private List<Photographer> GetPhotographers()
        {
            using (var context = new PhotoGalleryContext())
            {
                return context.Photographer.ToList();
            }
        }

        public void InserData()
        {
            Random rnd = new Random();
            var data = new List<Photographer>();
            for (int i = 0; i < 1500; i++)
            {
                data.Add(new Photographer
                {
                    Id = rnd.Next(),
                    Name = "name " + rnd.Next(300),
                    Status = "status " + rnd.Next(300),
                    Age = rnd.Next(100)
                });
            }
            index = 0;
            foreach (var obj in data)
            {
                db.SetAdd("photographers", "photographer " + index);
                db.SetAdd("photographerId" + index, obj.Id);
                db.SetAdd("photographerName" + index, obj.Name);
                db.SetAdd("photographerStatus" + index, obj.Status);
                db.SetAdd("photographerAge" + index, obj.Age);
                index++;
            }
        }

        public async Task<Tuple<List<Photographer>, string>> GetData()
        {
            var data = new List<Photographer>();
            var startTime = System.Diagnostics.Stopwatch.StartNew();
            await Task.Run(() =>
            {
                for (int i = 0; i < index; i++)
                {
                    var values = db.SetMembers("photographer" + i);
                    var model = new Photographer();
                    foreach (var value in values)
                    {
                        data.Add(new Photographer
                        {
                            Id = Convert.ToInt32(db.SetPop("photographerId" + i).ToString()),
                            Name = db.SetPop("photographerName" + i).ToString(),
                            Status = db.SetPop("photographerStatus" + i).ToString(),
                            Age = Convert.ToInt32(db.SetPop("photographerAge" + i).ToString())
                        });
                    }
                }
                startTime.Stop();
                Console.WriteLine("redis" + data.Count());
            });
            return Tuple.Create(data, startTime.Elapsed.ToString());
        }
    }
}
