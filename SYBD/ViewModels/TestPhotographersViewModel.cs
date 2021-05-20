using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SYBD.ViewModels
{
    public class TestPhotographersViewModel
    {
        public string TimePostgreSQL { get; set; }
        public string TimeMongoDb { get; set; }
        public string TimeRedis { get; set; }
        public string RequestText { get; set; }
    }
}
