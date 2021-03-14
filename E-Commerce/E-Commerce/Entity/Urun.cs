using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Entity
{

    public class Urun
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public string Resim { get; set; }
        public double Fiyat { get; set; }
        public int Stok { get; set; }
        public bool Slider { get; set; }
        public bool Anasayfa { get; set; }
        public bool Onay { get; set; }
        public bool OneCikan { get; set; }
        public int KategoriId { get; set; }
        public virtual Kategori Kategori  { get; set; }
    }
}