using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace E_Commerce.Identity
{
    public class IdentityInitializer:CreateDatabaseIfNotExists<IdentityDataContext>
    {
        protected override void Seed(IdentityDataContext context)
            //bu sayfada kullanıcı rollerini tanımladık admin veya user yetkileri verdik.
        {
            if (!context.Roles.Any(i=>i.Name=="Admin"))
            {
                var store = new RoleStore<ApplicationRol>(context);
                var manager = new RoleManager<ApplicationRol>(store);
                var role = new ApplicationRol() { Name = "Admin", Aciklama = "Admin Rolü" };
                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "User"))
            {
                var store = new RoleStore<ApplicationRol>(context);
                var manager = new RoleManager<ApplicationRol>(store);
                var role = new ApplicationRol() { Name = "User", Aciklama = "User Rolü" };
                manager.Create(role);
            }
            if (!context.Users.Any(i=>i.Ad=="Admin Admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Ad = "Admin", Soyad = "Admin", UserName = "AdminAdmin", Email = "admin@gmail.com" };
                manager.Create(user, "123456");
                manager.AddToRole(user.Id, "Admin");
                manager.AddToRole(user.Id, "User");
            }

            if (!context.Users.Any(i => i.Ad == "User User"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser() { Ad = "User", Soyad = "User", UserName = "UserUser", Email = "User@gmail.com" };
                manager.Create(user, "123456");
                manager.AddToRole(user.Id, "User");
            }
            base.Seed(context);
        }
    }
}