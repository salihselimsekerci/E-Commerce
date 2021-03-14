using E_Commerce.Entity;
using E_Commerce.Identity;
using E_Commerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        DataContext db = new DataContext();
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRol> RoleManager;
        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRol>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRol>(roleStore);
        }
        public ActionResult UserProfil()
        {
            //giriş yapan kullanıcının idsini verecek
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            //kullanıcıyı bulduk
            var user = UserManager.FindById(id);
            //kullanıcnın bilgilerini bulup getirecez
            var data = new UserProfile()
            {
                id = user.Id,
                Ad = user.Ad,
                Soyad = user.Soyad,
                Email = user.Email,
                KullanıcıAdi = user.UserName
               
            };
            return View(data);
        }
        [HttpPost]
        public ActionResult UserProfil(UserProfile model)
        {
            //idye göre getir.

            var user = UserManager.FindById(model.id);
            user.Ad = model.Ad;
            user.Soyad = model.Soyad;
            user.Email = model.Email;
            user.UserName = model.KullanıcıAdi;

            //bilgileri güncelle
            UserManager.Update(user);


            return View("Update");
        }
        public ActionResult LogOut()
        {
            //authentication oturumunu aldık
            var authManager = HttpContext.GetOwinContext().Authentication;
            //authentication işlemini kapadık
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login model,string returnUrl)
        {
            //kullanıcı var ise
            if (ModelState.IsValid)
            {
                //kullanıcıyı bul ad ve şifresi ile
                var user = UserManager.Find(model.KullanıcıAd, model.Sifre);
                //ve bulduysan kullanıcı sistemde var 
                if (user!=null)
                {
                    //kullanıcı bilgileri eşleşmişse authenticationmanager oluşturulur.
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    //usermanager nesnesi kullanılarak applicationcookie kabul eden bir claims nesnesi oluşturulur.
                    var Identityclaims = UserManager.CreateIdentity(user, "ApplicationCookie");
                    //authentication işlemleri detayları için authproperties nesnesi oluşturulur.
                    var authProperties = new AuthenticationProperties();
                    //oturumun devamlı açık olması ya da akapatılmasını sağlıyoruz.
                    authProperties.IsPersistent = model.Saklansinmi;
                    authManager.SignIn(authProperties,Identityclaims);
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Böyle Bir kullanıcı yok...");
                }
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Register model)
        {
            //kullanıcı bilgilerini girmiş ise 
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                //kullanıcının bilgilerini alıp kontrol ediyoruz.
                user.Ad = model.Ad;
                user.Soyad = model.Soyad;
                user.Email = model.Email;
                user.UserName = model.KullanıcıAdi;
                //kullanıcının şifresini alıyoruz 
                var result = UserManager.Create(user, model.Sifre);
                //kayıt başarılı ise
                if (result.Succeeded)
                {
                    //bu kullanıcıyada rollerini tanımladık 
                    if (RoleManager.RoleExists("user"))
                    {
                        //kullanıcıya bu rolü verdik
                        UserManager.AddToRole(user.Id, "user");
                    }
                    //kayıt olduktan sonra login sayfasına git
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı Oluştrma Hatası");
                }

            }
            return View(model);
        }
        public ActionResult YeniSifre()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult YeniSifre(SifreDegistirme model)
        {
            if (ModelState.IsValid)
            {
                //idye göre bul kullanıcının eski şifresini yeni şifre ile değiştir.
                var result = UserManager.ChangePassword(User.Identity.GetUserId(), model.EskiSifre, model.YeniSifre);
                return View("Update");
            }
            return View(model);
        }
        public ActionResult Index()
        {
            //kişi kullanıcı adına tıkladığında onun sipariş etiklerini listeledik.
            var username = User.Identity.Name;
            var order = db.Orders.Where(x => x.Username == username).Select(i => new UserOrder
            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                OrderState = i.OrderState,
                OrderDate = i.OrderDate,
                ToplamFiyat = i.ToplamFiyat
            }).OrderByDescending(i => i.OrderDate).ToList();
            return View(order);
        }
        public ActionResult Details(int id)
        {
            //sipariş detaylarını getirdik eşleştirmeleri atamaları yaptık.
            var model = db.Orders.Where(i => i.Id == id).Select(i => new OrderDetails
            {
                OrderId = i.Id,
                OrderNumber = i.OrderNumber,
                Total = i.ToplamFiyat,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Adres = i.Adres,
                Sehir = i.Sehir,
                Semt = i.Semt,
                Mahalle = i.Mahalle,
                PostaKodu = i.PostaKodu,
                OrderLines = i.OrderLines.Select(x => new OrderLineModel
                {
                    UrunId = x.UrunId,
                    Resim = x.Urun.Resim,
                    UrunAd = x.Urun.Ad,
                    Adet = x.Adet,
                    Fiyat = x.Fiyat
                }).ToList()
            }).FirstOrDefault();
            return View(model);
        }
        public PartialViewResult UserCount()
        {
            //kullanıcyı aldık view sayfasında kullanacağız
            var u = UserManager.Users;
            return PartialView(u);
        }
        public ActionResult UserList()
        {
            var u = UserManager.Users;
            return View(u);
        }
    }
}