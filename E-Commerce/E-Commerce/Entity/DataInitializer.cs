using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Entity
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var kategoriler = new List<Kategori>()
            {
                new Kategori(){Ad="KAMERA",Aciklama="KAMERA ÜRÜNLERİ"},
                new Kategori(){Ad="TELEFON",Aciklama="TELEFON ÜRÜNLERİ"},
                new Kategori(){Ad="BİLGİSAYAR",Aciklama="BİLGİSAYAR ÜRÜNLERİ"},
            };
            foreach (var kategori in kategoriler)
            {
                context.Kategoris.Add(kategori);
            }
            context.SaveChanges();
            var urunler = new List<Urun>()
            {
                new Urun(){Ad="Canon",Aciklama="Kamera Ürünleri",Fiyat=2500,Stok=125,Anasayfa=true,Onay=true,OneCikan=false,Slider=true,KategoriId=1,Resim="1.jpg" },
                new Urun(){Ad="Asus",Aciklama="Bilgisayar Ürünleri",Fiyat=2000,Stok=100,Anasayfa=true,Onay=true,OneCikan=true,Slider=true,KategoriId=3,Resim="2.jpg" },
                new Urun(){Ad="Lenova",Aciklama="Bilgisayar Ürünleri",Fiyat=3500,Stok=50,Anasayfa=false,Onay=true,OneCikan=true,Slider=false,KategoriId=3,Resim="3.jpg" },
                new Urun(){Ad="Samsung S6",Aciklama="Telefon Ürünleri",Fiyat=5000,Stok=150,Anasayfa=true,Onay=true,OneCikan=true,Slider=true,KategoriId=2,Resim="4.jpg" },
            };
            foreach (var urun in urunler)
            {
                context.Uruns.Add(urun);
            }
            context.SaveChanges();
            base.Seed(context);
        }


    }
}