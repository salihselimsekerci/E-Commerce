using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Identity
{
    public class ApplicationRol:IdentityRole
    {
        public string Aciklama { get; set; }
        public ApplicationRol()
        {

        }

        public ApplicationRol(string rolename,string Aciklama)
        {
            this.Aciklama = Aciklama;
        }
    }
}