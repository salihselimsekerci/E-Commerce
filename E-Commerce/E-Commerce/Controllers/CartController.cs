using E_Commerce.Entity;
using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        DataContext db=new DataContext();
       
        public ActionResult Index()
        {
            return View(GetCart());
        }
        public ActionResult AddToCart(int id)
        {
            //kullanıcının sepete eklemek istediği ürün idyi bul
            var urun = db.Uruns.FirstOrDefault(x => x.Id == id);
            if (urun!=null)//eğer null değil ise kullanıcı sepete bir ürün ekleyecektir.
            {
                GetCart().AddUrun(urun, 1);//bir ürün ekledik,
            }
            return RedirectToAction("Index");
        }
       public Cart GetCart()
        {
            //cart için bir oturum açtık orada sakladık
            var cart = (Cart)Session["Cart"];
            //cart null ise kart yok ise 
            if (cart==null)
            {
                //kullancıya yeni cart ekle
                cart = new Cart();
                //sessionda sakla kullanıcıya  ona özel cart oluşturduk 
                Session["Cart"] = cart;
            }
            return (cart);
        }
        public ActionResult SilSepet(int id)
        {
            //silinecek ürünü bulduk
            var urun = db.Uruns.FirstOrDefault(x => x.Id == id);
            if (urun!=null)
            {
                GetCart().SepetSil(urun);

            }
            return RedirectToAction("Index");

        }
        public PartialViewResult ToplamTutar()
        {
            return PartialView(GetCart());
        }
        public PartialViewResult ToplamTutarMenu()
        {
            return PartialView(GetCart());
        }
        public ActionResult SiparisDetay()
        {
            return View(new Siparis());
        }
        [HttpPost]
        public ActionResult SiparisDetay(Siparis model)
        {
            //cart classımızı çağırdık
            var cart = GetCart();
            //sepette ürün yoksa
            if (cart.CartLines.Count==0)
            {
                //bu uyarıyı ver
                ModelState.AddModelError("Ürün Yok", "Sepetinizde ürün bulunmamaktadır.");
            }
            //zorunlu alanları doldurup sipariş verdiyse
            if (!ModelState.IsValid)
            {
                SiparisKaydet(cart, model);
                //sepet sayfasını temizle ve uyarı mesajını return ile döndür.
                cart.Clear();
                return View("Tamamlandi");
            }
            else
            {
                return View(model);
            }
            

           
        }

        private void SiparisKaydet(Cart cart, Siparis model)
        {
            var order = new Order();
            order.OrderNumber = "A" + (new Random()).Next(1111, 9999).ToString();
            order.ToplamFiyat = cart.ToplamFiyat();
            order.OrderDate = DateTime.Now;
            order.Username = User.Identity.Name;
            order.OrderState = OrderState.Bekleniyor;
            order.Adres = model.Adres;
            order.Sehir = model.Sehir;
            order.Semt = model.Semt;
            order.Mahalle = model.Mahalle;
            order.PostaKodu = model.PostaKodu;
            order.OrderLines = new List<OrderLine>();
            foreach (var item in cart.CartLines)
            {
                var orderline = new OrderLine();
                orderline.Adet = item.Adet;
                orderline.Fiyat = item.Adet * item.Urun.Fiyat;
                orderline.UrunId = item.Urun.Id;
                order.OrderLines.Add(orderline);
            }
            db.Orders.Add(order);
            db.SaveChanges();
        }
       
    }
}