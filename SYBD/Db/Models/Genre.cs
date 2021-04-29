using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SYBD.Db.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Photo = new HashSet<Photo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Photo> Photo { get; set; }
    }
}
