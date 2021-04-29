using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SYBD.Db.Models
{
    public partial class Photo
    {
        public Photo()
        {
            Income = new HashSet<Income>();
            Repository = new HashSet<Repository>();
            Sale = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public int? Photographerid { get; set; }
        public DateTime Photodate { get; set; }
        public string Quality { get; set; }
        public int? Rating { get; set; }
        public decimal Price { get; set; }
        public int? Genreid { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Photographer Photographer { get; set; }
        public virtual ICollection<Income> Income { get; set; }
        public virtual ICollection<Repository> Repository { get; set; }
        public virtual ICollection<Sale> Sale { get; set; }
    }
}
