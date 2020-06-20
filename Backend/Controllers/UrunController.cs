using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Backend.Controllers
{
    public class UrunController:ApiController
    {
        beunyemekEntities _ent = new beunyemekEntities();
        [HttpGet]
        public List<UrunTip> UrunleriGetir()
        {
            return _ent.urun.Select(p => new UrunTip()
            {
                urunID = p.urunID,
                urunAd = p.urunAd,
                urunFiyat = p.urunFiyat
            
            }).ToList();
        }
        [HttpPost]
        public List<UrunTip> UrunEkle(urun kayit)
        {
            try
            {
                urun d = new urun();
                d.urunID = kayit.urunID;
                d.urunAd = kayit.urunAd;
                d.urunFiyat = kayit.urunFiyat;
               
                _ent.urun.Add(d);
                _ent.SaveChanges();
                return UrunleriGetir();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpGet]
        public List<UrunTip> UrunSil(int urunID)
        {
            List<siparis> s = _ent.siparis.Where(p => p.urunID == urunID).ToList();
            if (s != null)
            {
                _ent.siparis.RemoveRange(s);
                _ent.SaveChanges();
            }
            _ent.urun.Remove(_ent.urun.Find(urunID));
            _ent.SaveChanges();
            return UrunleriGetir();
        }

    }
    public class UrunTip
    {
        public int urunID { get; set; }
        public string urunAd { get; set; }
        public int urunFiyat { get; set; }
    }
}