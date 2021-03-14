using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class UserProfile
    {
        public string id { get; set; }
        [Required(ErrorMessage ="Boş Geçilemez")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Soyad { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string KullanıcıAdi { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [EmailAddress(ErrorMessage ="Geçerli Email Adresi Girin")]
        public string Email { get; set; }
    }
}