using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Backend.Controllers
{
    public class SiparisController:ApiController
    {
        beunyemekEntities _ent = new beunyemekEntities();
        [HttpGet]
        public List<SiparisTip> RezervasyonlariGoster (int ogrenciID)
        {
            return _ent.siparis.Where(p => p.ogrenciID == ogrenciID).Select(p => new SiparisTip()
            {
                urunAd = p.urun.urunAd,
                urunFiyat = p.urun.urunFiyat,
                urunID = p.urunID,
                siparisID = p.siparisID,
                ogrenciID = p.ogrenciID

            }).ToList();
        }
        [HttpPost]
        public List<SiparisTip> YeniRezervasyon(SiparisTip veri)
        {
            try
            {
                siparis d = new siparis();
                d.ogrenciID = veri.ogrenciID;
                d.urunID = veri.urunID;
                _ent.siparis.Add(d);
                _ent.SaveChanges();
                return RezervasyonlariGoster(veri.ogrenciID);

            }
            catch(Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        public List<SiparisTip> SiparisSil (int siparisID)
        {
            try
            {
                siparis d = _ent.siparis.Find(siparisID);
                int ogrenciid = d.ogrenciID;
                _ent.siparis.Remove(d);
                _ent.SaveChanges();
                return RezervasyonlariGoster(ogrenciid);
                

            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
    public class SiparisTip
    {
        public int siparisID { get; set; }
        public int ogrenciID { get; set; }
        public int urunID { get; set; }
        public long ogrenciNo { get; set; }
        public string ogrenciAd { get; set; }
        public string ogrenciSoyad { get; set; }
        public string urunAd { get; set; }
        public int urunFiyat { get; set; }
    }
}