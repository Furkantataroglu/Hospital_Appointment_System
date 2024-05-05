using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace HastaneRandevuSistemi.Controllers
{
    [Authorize(Roles = "Hasta")]
    public class RandevuController : Controller
    {
        

        private MyContext db = new MyContext();

       
        public IActionResult RandevuAl()
        {
           
         
         
            ViewBag.Sehirler = SehirGetir();
            ViewBag.ilceler = IlceGetir();

            //RandevuModel model = new RandevuModel();


            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult RandevuAl(RandevuModel model)
        {
           
            ViewBag.Sehirler = SehirGetir();
            ViewBag.ilceler = IlceGetir();
            
            var authList = User.Claims.ToList();
            var mail = authList[0].Value;
            var tcNo = db.KullaniciTablosu
                .Where(d => d.KullaniciMail == mail)
                .Select(d => d.KullaniciTcNo)
                .FirstOrDefault();
            var ad = db.KullaniciTablosu
                .Where(d => d.KullaniciMail == mail)
                .Select(d => d.KullaniciAd)
                .FirstOrDefault();
            var soyad = db.KullaniciTablosu
                .Where(d => d.KullaniciMail == mail)
                .Select(d => d.KullaniciSoyad)
                .FirstOrDefault();
            var doktorad = db.DoktorTablosu
               .Where(d => d.DoktorId == model.DoktorNO)
               .Select(d => d.DoktorAdi)
               .FirstOrDefault();
            var doktorsoyad = db.DoktorTablosu
              .Where(d => d.DoktorId == model.DoktorNO)
              .Select(d => d.DoktorSoyad)
              .FirstOrDefault();

            var gun = db.doktorCalismaSaatlariModeliTablosu
                .Where(d => d.DCSMId == Int32.Parse(model.Gun))
                .Select(d => d.DCSMcalismaTarihi).FirstOrDefault();
            var hasta = new RandevuModel
            {
                HastaTcNo=tcNo,
                HastaAdi=ad,
                HastaSoyadi=soyad,
                DoktorNO= model.DoktorNO,
                DoktorAdi= doktorad,
                DoktorSoyadi= doktorsoyad,
                Gun =gun,
                Saat=model.Saat              
               
            };

            if (hasta.Saat == "--Saat secin--" && hasta.Saat == "--Saat yok--")
            {
                ViewBag.msg = "Saat Boş Olamaz.";
            }
            else
            {
                db.Add(hasta);
                db.SaveChanges();
                return View();
            }
            return View();
        }






        public JsonResult GetSehir()
        {
            var sehir = db.SehirTablosu.OrderBy(p => p.SehirAdi).ToList();
            return new JsonResult(sehir);
        }

        public JsonResult Getilce(int id)
        {
            var ilce = db.IlceTablosu.Where(p=>p.Sehirid == id).OrderBy(p => p.IlceAdi).ToList();
            return new JsonResult(ilce);

        }

        public JsonResult GetHastane(int id)
        {
            var hastaneler = db.HastaneTablosu.Where(p => p.ilceid == id).OrderBy(p => p.HastaneAdi).ToList();
            return new JsonResult(hastaneler);
        }


        public JsonResult GetBolum()
        {
            var bolumler = db.BolumTablosu.OrderBy(p=>p.BolumAdi).ToList();
            return new JsonResult(bolumler);
        }

        public JsonResult GetDoktor(int bolumid, int hastaneid)
        {
            var doktorlar = db.DoktorTablosu.Where(p => p.BolumId == bolumid && p.hastanelerId == hastaneid).OrderBy(p => p.DoktorAdi).ToList();
            return new JsonResult(doktorlar);
        }

        public JsonResult GetGun(int doktorid)
        {
            var gunler =db.doktorCalismaSaatlariModeliTablosu.Where(p=>p.DoktorId==doktorid).ToList();
            

            // 7 günlük haftalık dilim getirme
            DateTime now = DateTime.Now;
            DateTime oneWeekEarlier = now.AddDays(-7); 

            List<string> dateRangeList = new List<string>();

            while (now >= oneWeekEarlier) 
            {
                now = now.AddDays(-1);
                dateRangeList.Add(now.ToString("yyyy-MM-dd"));
                 
            }

            gunler = gunler.Where(item => !dateRangeList.Contains(item.DCSMcalismaTarihi))
                    .ToList();
            return new JsonResult(gunler);
        }
        static bool IsDateInRange(string dateString, List<string> dateRangeList)
        {
            DateTime date = DateTime.ParseExact(dateString, "MM.dd.yyyy", CultureInfo.InvariantCulture);
            return dateRangeList.Contains(date.ToString("MM.dd.yyyy"));
        }
        public JsonResult GetSaat(int dcsmId)
        {
            var baslangicsaat = db.doktorCalismaSaatlariModeliTablosu
                .Where(d => d.DCSMId == dcsmId)
                .Select(d => d.DCSMBaslangicSaati)
                .FirstOrDefault();
            var bitissaat = db.doktorCalismaSaatlariModeliTablosu
                .Where(d => d.DCSMId == dcsmId)
                .Select(d => d.DCSMBitisSaati)
                .FirstOrDefault();
            var dcsmtarih = db.doktorCalismaSaatlariModeliTablosu
                .Where(d => d.DCSMId == dcsmId)
                .Select(d => d.DCSMcalismaTarihi)
                .FirstOrDefault();
            var dcsmdoktorid = db.doktorCalismaSaatlariModeliTablosu
                .Where(d => d.DCSMId == dcsmId)
                .Select(d => d.DoktorId)
                .FirstOrDefault();

            List<string> dilimler = new List<string>();

            // Başlangıç ve bitiş saatlerini TimeSpan türüne çevirme
            TimeSpan baslangic = TimeSpan.Parse(baslangicsaat);
            TimeSpan bitis = TimeSpan.Parse(bitissaat);

            // Dilimleme işlemleri
            TimeSpan suAnkiZaman = DateTime.Now.TimeOfDay;
            var now = DateTime.Now.ToString();
            // Şu anki zamanı ve daha önceki zamanları kontrol etmek için
            if (now == dcsmtarih)
            {
                if (baslangic < suAnkiZaman)
                {
                    while (baslangic < suAnkiZaman)
                    {
                        baslangic = baslangic.Add(TimeSpan.FromMinutes(15));
                    }
                    if (baslangic >= suAnkiZaman)
                    {
                        // Dilimleme işlemleri
                        TimeSpan simdikiSaat = baslangic;
                        while (simdikiSaat < bitis)
                        {
                            dilimler.Add(simdikiSaat.ToString(@"hh\:mm"));
                            simdikiSaat = simdikiSaat.Add(TimeSpan.FromMinutes(15));
                        }
                    }
                }
                else
                {
                    TimeSpan simdikiSaat = baslangic;
                    while (simdikiSaat < bitis)
                    {
                        dilimler.Add(simdikiSaat.ToString(@"hh\:mm"));
                        simdikiSaat = simdikiSaat.Add(TimeSpan.FromMinutes(15));
                    }
                }
            }
            else
            {
                while (baslangic < bitis)
                {
                    dilimler.Add(baslangic.ToString(@"hh\:mm"));
                    baslangic = baslangic.Add(TimeSpan.FromMinutes(15));
                }
            }

            //burası randevu tablosundan doktor id çekme.
            var saatler = db.RandevuTablosu
                .Where(d => d.DoktorNO == dcsmdoktorid && d.Gun == dcsmtarih)
                .Select(d => d.Saat).ToList();



            if (saatler.Count==0){
                return new JsonResult(dilimler);
            }
            else {

                var kalanDilimler = dilimler.Except(saatler).ToList();
                return new JsonResult(kalanDilimler);
               
            }

        }


















        public List<KullaniciModel> kullaniciGetir()
        {
            List<KullaniciModel> kullaniciListesi = db.KullaniciTablosu.ToList();
            return kullaniciListesi;

        }
        public List<HastanelerModel> hastaneGetir()
        {
            List<HastanelerModel> hastaneListesi = db.HastaneTablosu.ToList();
            return hastaneListesi;

        }
        public List<IlceModel> IlceGetir() 
        {
            List<IlceModel> ilceListesi = db.IlceTablosu.ToList();

            //  List<string> ilceListesi = db.IlceTablosu.Where(p=>p.Sehirid==sehirid).Select(p=>p.IlceAdi).ToList(); //parametre:şehir id  return type list<string>

            return ilceListesi;

        }
        public List<string> IlceGetir(int sehirid)
        {
            List<string> ilceListesi = db.IlceTablosu.Where(p=>p.Sehirid==sehirid).Select(p=>p.IlceAdi).ToList(); //parametre:şehir id  return type list<string>
            return ilceListesi;
        }
        public List<SehirModel> SehirGetir()
        {
            List<SehirModel> sehirListesi = db.SehirTablosu.ToList();
            return sehirListesi;

        }
        public List<DoktorModel> DoktorGetir()
        {
            List<DoktorModel> doktorListesi = db.DoktorTablosu.ToList();
            return doktorListesi;

        }


    }
}
