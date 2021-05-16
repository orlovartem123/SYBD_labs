using System;

namespace SYBD.Db.MongoDb.Models
{
    public class Sale
    {
        public string Id { get; set; }
        public DateTime SoldDate { get; set; }
        public string PhotoId { get; set; }
        public int Count { get; set; }
        public string StockId { get; set; }
    }
}
