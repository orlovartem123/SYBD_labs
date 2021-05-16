using System;

namespace SYBD.Db.MongoDb.Models
{
    public class Photo
    {
        public string Id { get; set; }
        public string PhotographerId { get; set; }
        public string GenreId { get; set; }
        public DateTime PhotoDate { get; set; }
        public string Quality { get; set; }
        public int Rating { get; set; }
        public decimal Price { get; set; }
    }
}
