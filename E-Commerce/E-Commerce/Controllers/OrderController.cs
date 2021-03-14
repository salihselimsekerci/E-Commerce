using E_Commerce.Entity;
using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            var orders = db.Orders.Select(i => new AdminOrder
            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                ToplamFiyat = i.ToplamFiyat,
                Count = i.OrderLines.Count

            }).OrderByDescending(i => i.OrderDate).ToList();
            return View(orders);
        }
        public ActionResult Details(int id)
        {
            var model = db.Orders.Where(i => i.Id == id).Select(i => new OrderDetails
            {
                OrderId = i.Id,
                OrderNumber = i.OrderNumber,
                Total = i.ToplamFiyat,
                Username = i.Username,
                OrderDate = i.OrderDate,
                OrderState = i.OrderState,
                Adres = i.Adres,
                Sehir = i.Sehir,
                Semt = i.Semt,
                Mahalle = i.Mahalle,
                PostaKodu = i.PostaKodu,
                OrderLines = i.OrderLines.Select(x => new OrderLineModel()
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
        public ActionResult UpdateOrderState(int OrderId,OrderState Orderstate)
        {
            //orders veri tabanına git tıklanan idyi bul
            var order = db.Orders.FirstOrDefault(i => i.Id == OrderId);
            //tıklanan id var ise boş değil ise
            if (order!=null)
            {
                //sipariş durumunu al ve order state modelgncelle
                order.OrderState = Orderstate;
                // kaydet
                db.SaveChanges();
                //bu şekilde uyarı mesajı ver.
                TempData["mesaj"] = "Bilgiler Kaydedildi";
                return RedirectToAction("Details", new { id = OrderId });
            }
            return RedirectToAction("Index");
        }
        public  ActionResult BekleyenSiparisler()
        {
            //orders veritabanından durumu bekleniyor olanları getir ve listele
            var model = db.Orders.Where(i => i.OrderState == OrderState.Bekleniyor).ToList();
            return View(model);
        }
        public ActionResult TamamlananSiparisler()
        {
            //orders veritabanından durumu bekleniyor olanları getir ve listele
            var model = db.Orders.Where(i => i.OrderState == OrderState.Tamamlandı).ToList();
            return View(model);
        }
        public ActionResult PaketlenenSiparisler()
        {
            //orders veritabanından durumu bekleniyor olanları getir ve listele
            var model = db.Orders.Where(i => i.OrderState == OrderState.Paketlendi).ToList();
            return View(model);
        }
        public ActionResult KargolananSiparisler()
        {
            //orders veritabanından durumu bekleniyor olanları getir ve listele
            var model = db.Orders.Where(i => i.OrderState == OrderState.Kargolandı).ToList();
            return View(model);
        }
    }
}