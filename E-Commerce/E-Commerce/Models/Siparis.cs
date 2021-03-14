using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Siparis
    {
        [Required(ErrorMessage ="Boş Geçilemez")]
        public string KullaniciAdi { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Adres { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Sehir { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Semt { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Mahalle { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string PostaKodu { get; set; }
      
       
    }
}