using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Backend.Controllers
{
    public class GirisController:ApiController
    {
        beunyemekEntities _ent = new beunyemekEntities();
        [HttpGet]
        public int AdminGiris(string aka, string apsw)
        {
            admin l = _ent.admin.FirstOrDefault(g => g.adminUsername == aka && g.adminPassword == apsw);
            if (l != null)
            {
                return l.adminID;
            }
            else
            {
                return 0;
            }
        }
        public int OgrenciGiris(long oka, string opsw)
        {
            ogrenci o = _ent.ogrenci.FirstOrDefault(a => a.ogrenciNo == oka && a.ogrenciAd == opsw);
            if(o != null)
            {
                return o.ogrenciID;
            }
            else
            {
                return 0;
            }
        }
    }
}