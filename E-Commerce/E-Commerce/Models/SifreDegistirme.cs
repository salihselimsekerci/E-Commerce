using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class SifreDegistirme
    {
        [Required (ErrorMessage ="Boş Geçilemez")]
        public string EskiSifre { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [StringLength(100,MinimumLength =5,ErrorMessage ="Şifreniz en az 5 karakter olmalı...")]
        public string YeniSifre { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        [Compare("YeniSifre",ErrorMessage ="Şifreler aynı değil")]
        public string YeniSifreTekrar { get; set; }
    }
}