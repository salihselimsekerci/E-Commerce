using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Soyad { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string KullanıcıAdi { get; set; }
        [Required (ErrorMessage ="Boş Geçilemez")]
        [EmailAddress(ErrorMessage ="Geçersiz Email Adresi...")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Sifre { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Compare("Sifre",ErrorMessage ="Şifreler Aynı Değil")]
        public string SifreTekrar { get; set; }
    }
}