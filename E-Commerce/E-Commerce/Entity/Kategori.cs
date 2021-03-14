using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Entity
{
    public class Kategori
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public virtual List<Urun> Uruns { get; set; }
    }
}