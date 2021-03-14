using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Entity;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        // GET: Home
        public PartialViewResult PopulerUrunler()
        {
            //ürünler tablosundan hem popüler hem onaylı olan ürünleri getir ama ilk 5 tanasini getir.
            return PartialView(db.Uruns.Where(x => x.Onay && x.OneCikan).Take(5).ToList());
        }
        public ActionResult Search(string q)
        {
            //onaylı ürünlere göre ara
            var p = db.Uruns.Where(x => x.Onay == true);
            //eğer qnun değeri null veya boş değil ise arama yapar
            if (!string.IsNullOrEmpty(q))
            {
               // eğer qya bir ad veya açıklamadan gelen bir veriyi aramaya kalkarsak onu arar
                p = p.Where(x => x.Ad.Contains(q) || x.Aciklama.Contains(q));
               
            }
            return View(p.ToList());
        }
        public PartialViewResult Slider()
        {
            //Ürünler tablosundan hem onaylı hemde sadece 5 tane slider listelensin diyorum.
            return PartialView(db.Uruns.Where(x => x.Onay && x.Slider).Take(5).ToList());
        }
        public ActionResult Index()
        {
            //veri tabanından anasayfa ve onaylı olanları anasayfaya listeledik getirdik.
            return View(db.Uruns.Where(x=>x.Anasayfa&&x.Onay).ToList());
        }
     public ActionResult UrunDetay(int id)//id tıklanan ürünün idsini alır yani idye göre işlem yapacağımız için bunu belirtmeliyiz sayfadan sayfaya o ürünün idsini alarak geçiş yapacak
        {
            //urunler veritabanından tıklanan ürünün idsi ile veri tabanından gelen id eşit ise firstor ile ara bul ve onu getir
            return View(db.Uruns.Where(x=>x.Id==id).FirstOrDefault());
        }
        public ActionResult Urun()
        {
            //Tüm ürünleri listeledik
            return View(db.Uruns.ToList());
        }
        public ActionResult UrunList(int id)
        {
            //tıklanan kategoriye ait kaç tane ürün var ise onları listeleyecek
            return View(db.Uruns.Where(x => x.KategoriId == id).ToList());
        }
    }
}