using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Login
    {
        [Required (ErrorMessage ="Boş Geçilemez")]
        public string KullanıcıAd { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Sifre { get; set; }
        public bool Saklansinmi { get; set; }
    }
}