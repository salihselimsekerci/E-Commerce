using E_Commerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class Cart
    {
        private List<CartLine> _CartLines = new List<CartLine>();
        public List<CartLine> CartLines 
        {
            get { return _CartLines; }
        }

        public void AddUrun(Urun urun,int adet)
        {
            //sepet listesinde ara neye göre UrunId ile eklenen ürüne göre 
            var line = _CartLines.FirstOrDefault(x => x.Urun.Id == urun.Id);
            //sepet boş değil ise
            if (line==null)
            {
                //sepetlistesine ekle urun ve adeti ekle
                _CartLines.Add(new CartLine() { Urun = urun, Adet = adet });
            }
            else
            {
                //var olan ürün sayısını kullanıcının eklemek istediği ürüne göre sepet sayısını ekle
                line.Adet += adet;
            }
        }

        public void SepetSil(Urun urun)
        {
            //sepet listesindeki idye göre kullanıcının silmek istediği idye göre sadece o ürünü sil 
            _CartLines.RemoveAll(x => x.Urun.Id == urun.Id);
        }
        public double ToplamFiyat()
        {
            //toplam fiyat için urun fiyati ile adet sayısını çarparız sum methodu toplama işlemi yapar.
            return _CartLines.Sum(x => x.Urun.Fiyat * x.Adet);
        }
        public void Clear()
        {
            //sepeti temizledik.
            _CartLines.Clear();
        }
    }
    public class CartLine
    {
        public Urun Urun { get; set; }
        public int Adet { get; set; }
    }
}