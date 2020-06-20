using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Backend.Controllers
{
    public class OgrenciController:ApiController
    {
        beunyemekEntities _ent = new beunyemekEntities();
        [HttpGet]
        public List<OgrenciTip>OgrencileriGetir()
        {
            return _ent.ogrenci.Select(p => new OgrenciTip()
            {
                ogrenciID = p.ogrenciID,
                ogrenciNo = p.ogrenciNo,
                ogrenciAd = p.ogrenciAd,
                ogrenciSoyad = p.ogrenciSoyad,
                ogrenciFakulte = p.ogrenciFakulte,
                ogrenciBolum = p.ogrenciBolum
            }).ToList();
        }
        [HttpPost]
        public List<OgrenciTip> OgrenciEkle (ogrenci kayit)
        {
            try
            {
                ogrenci d = new ogrenci();
                d.ogrenciID = kayit.ogrenciID;
                d.ogrenciNo = kayit.ogrenciNo;
                d.ogrenciAd = kayit.ogrenciAd;
                d.ogrenciSoyad = kayit.ogrenciSoyad;
                d.ogrenciFakulte = kayit.ogrenciFakulte;
                d.ogrenciBolum = kayit.ogrenciBolum;
                _ent.ogrenci.Add(d);
                _ent.SaveChanges();
                return OgrencileriGetir();
            }
            catch(Exception ex)
            {
                return null;
            }

        }
        [HttpGet]
        public List<OgrenciTip> OgrenciSil(int ogrenciID)
        {
            List<siparis> s = _ent.siparis.Where(p => p.ogrenciID == ogrenciID).ToList();
            if (s!=null)
            {
                _ent.siparis.RemoveRange(s);
                _ent.SaveChanges();
            }
            _ent.ogrenci.Remove(_ent.ogrenci.Find(ogrenciID));
            _ent.SaveChanges();
            return OgrencileriGetir();
        }
    }
    public class OgrenciTip
    {
        public int ogrenciID { get; set; }
        public long ogrenciNo { get; set; }
        public string ogrenciAd { get; set; }
        public string ogrenciSoyad { get; set; }
        public string ogrenciFakulte { get; set; }
        public string ogrenciBolum { get; set; }


    }

}