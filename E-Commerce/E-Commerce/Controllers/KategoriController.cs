using System;
using E_Commerce.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;

namespace E_Commerce.Controllers
{
  
    public class KategoriController : Controller
    {
        // GET: Kategori
        DataContext db = new DataContext();
        ///partial view taşınabilir sayfadır yani bir layouta bağlı olmaz layout sitedeki sabit alanlardır.Menu ve footer gibi bu partial viewimizde
        ///her sayfada çağırıp istediğimiz sayfada görünmesini sağlarız.
        public PartialViewResult _KategoriListe()
        {
            ///kategori tablosu ile kategorimodel arasında ilişki kurduk 
            var kategoriler = db.Kategoris.Select(x => new KategoriModel()

            {
                //Idsı ile eşitledik
                Id = x.Id,
                //Adı ile eşitledik
                Ad = x.Ad,
                //count miktarını ise urunlerin sayısı geleceği için urun tablosunun countunu yani ne kadar ürün varsa sayısını aldık.
                Count = x.Uruns.Count
            }
            ).ToList(); 
            return PartialView(kategoriler);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //kategorileri listeledik
            
            return View(db.Kategoris.ToList());
        }
        //burası get sayfası sayfanın tasarımı için kullanacağız yani sayfayı burası yükleyecek
        [Authorize(Roles = "Admin")]
        public ActionResult Ekle()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost] //post ise bir buttona tıklandığında veri eklemek için kullanacağız
        public ActionResult Ekle(Kategori data)
        {
            db.Kategoris.Add(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //guncelleme sayfasına tıklandıgında guncellenecek olan veriyi taşıdık. find ile tıklanan idyi bulduk
        [Authorize(Roles = "Admin")]
        public ActionResult Guncelle(int id)
        {
            var guncelle = db.Kategoris.Find(id);

            return View("Guncelle",guncelle);
        }
        //guncellenme işlemi oldugunda çalışacak
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Guncelle(Kategori data)
        {
            //kullanıcının tıkladığı satırdaki verinin idsini bul
            var guncelle = db.Kategoris.Find(data.Id);
            //o idye göre açıklama kolonunu düzenle
            guncelle.Aciklama = data.Aciklama;
            //adı düzenle
            guncelle.Ad = data.Ad;
            //veri tabanına kaydet
            db.SaveChanges();
            //işlem bitince anasayfaya at kategori anasayfasına
            return RedirectToAction("Index");

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Sil(int id)
        {
            //tıklanan id değerindeki kolonu bul
            var sil = db.Kategoris.Find(id);
            //dbden veri tabanından ve sayfadan sil
            db.Kategoris.Remove(sil);
            //kaydet
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}