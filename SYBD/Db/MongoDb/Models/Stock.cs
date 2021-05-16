using System;
using System.Collections.Generic;

namespace SYBD.Db.MongoDb.Models
{
    public class Stock
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public List<Tuple<string, int>> Photos { get; set; }
    }
}
