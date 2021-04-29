// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SYBD.Db.Models
{
    public partial class Repository
    {
        public int Id { get; set; }
        public int Stockid { get; set; }
        public int Photoid { get; set; }
        public int Count { get; set; }

        public virtual Photo Photo { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
