using System;
using System.Collections.Generic;
using E_Commerce.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace E_Commerce.Controllers
{
    [Authorize(Roles ="Admin")]//rolü admin olan erişebilsin güncelleme yapabilsin.
    public class UrunController : Controller
    {
        // GET: Urun
      
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            
            return View(db.Uruns.ToList());
        }
        public ActionResult Ekle()
        {
            List<SelectListItem> deger1 = (from i in db.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Ad,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Urun data,HttpPostedFileBase Resim)
        {
            string path = Path.Combine("~/Content/İmage/", Resim.FileName);
            Resim.SaveAs(Server.MapPath(path));
            data.Resim = Resim.FileName.ToString();
            db.Uruns.Add(data);
            db.SaveChanges();
            return RedirectToAction("Index");
            //var dosyaadi = Path.GetFileName(Resim.FileName);
            //var uzanti = Path.GetExtension(Resim.FileName);
            //string adi = Path.GetFileNameWithoutExtension(dosyaadi);
            //var yol = Path.Combine(Server.MapPath("~/Content/İmage/"), dosyaadi + uzanti);
            //Resim.SaveAs(yol);
            //db.Uruns.Add(data);
            

           


        }
        public ActionResult Guncelle(int id)
        {

            List<SelectListItem> deger1 = (from i in db.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Ad,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var guncelle = db.Uruns.Find(id);
            return View("Guncelle", guncelle);
        }
        [HttpPost]
        public ActionResult Guncelle(Urun data)
        {
            var guncelle = db.Uruns.Find(data.Id);
            guncelle.Aciklama = data.Aciklama;
            guncelle.Ad = data.Ad;
            guncelle.Onay = data.Onay;
            guncelle.OneCikan = data.OneCikan;
            guncelle.Slider = data.Slider;
            guncelle.Stok = data.Stok;
            guncelle.Kategori = data.Kategori;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var sil = db.Uruns.Find(id);
            db.Uruns.Remove(sil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}