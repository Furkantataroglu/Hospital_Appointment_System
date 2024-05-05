using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace HastaneRandevuSistemi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private MyContext db = new MyContext();
        public IActionResult CikisYap()
        {
            
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
          
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public IActionResult Dashboard() { 
        
            return View();
        }
        public List<DoktorModel> DoktorGetir()
        {
            List<DoktorModel> doktorListesi = db.DoktorTablosu.ToList();
            return doktorListesi;
        }
        public IActionResult Doktor()
        {
            var doktorlar = db.DoktorTablosu.Include(d => d.bolum).Include(d=>d.hastaneler)
            .ToList();
            return View(doktorlar);
        }
        [HttpGet]       
        public IActionResult Edit(int id)
        {
            ViewBag.hastaneler = HastaneGetir();
            ViewBag.bolum = BolumGetir();
            var Model = db.DoktorTablosu.Find(id);
            if (Model == null)
            {
                return NotFound();
            }
            ViewBag.id=id;
            return View(Model);
        }
        [HttpPost]
        public IActionResult Edit(int id, DoktorModel Doktor)
        {
            ViewBag.hastaneler = HastaneGetir();
            ViewBag.bolum = BolumGetir();
            Doktor.DoktorId=id;
           

            if (ModelState.ErrorCount==3) 
            {
               var Mail =  mailgetir(id);
                if (Mail.Any(p => p.DoktorMail == Doktor.DoktorMail))
                {
                    ViewBag.msg = "Bu mail kullanılıyor.";
                    return View(Doktor);
                }
                else
                {

                    db.Update(Doktor);
                    db.SaveChanges();
                    return RedirectToAction("Doktor");
                }
            }
            else
            {
                return View(Doktor);
            }
        }
        public IActionResult Delete(int id)
        {
            var doktor = db.DoktorTablosu.Find(id);
            if (doktor == null)
            {
                return NotFound(); // Öğe bulunamazsa 404 Not Found döndür
            }
            return View(doktor);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteComplete(int id)
        {
            var doktor = db.DoktorTablosu.Find(id);
            if (doktor == null)
            {
                return NotFound(); // Öğe bulunamazsa 404 Not Found döndür
            }
            db.DoktorTablosu.Remove(doktor);
            db.SaveChanges();

            return RedirectToAction("Doktor");    
        }
        public IActionResult DoktorEkle()
        {
            ViewBag.BolumId = BolumGetir();
            ViewBag.hastaneler = HastaneGetir();
            return View();

        }
        [HttpPost]
        public IActionResult DoktorEkle(DoktorModel y)
        {
            ViewBag.BolumId = BolumGetir();
            ViewBag.hastaneler = HastaneGetir();
            var tcler = db.KullaniciTablosu.Any(p => p.KullaniciTcNo == y.DoktorTcNo);
            if (ModelState.ErrorCount==2 && tcler ==false)
            {
                if (db.DoktorTablosu.Any(p => p.DoktorTcNo == y.DoktorTcNo))
                {
                    ViewBag.msg = "TcNo hatalı...";
                    return View(y);
                }
                else if (db.DoktorTablosu.Any(p => p.DoktorMail == y.DoktorMail))
                {
                    ViewBag.msg = "Mail hatalı...";
                    return View(y);
                }
                db.Add(y);
                db.SaveChanges();
                return RedirectToAction("Doktor");
            }
            else
            {
                ViewBag.msg = "EKLENEMEDİ...";
                return View(y);
            }
        }
        public List<BolumModel> BolumGetir()
        {
            List<BolumModel> bolumListesi = db.BolumTablosu.ToList();
            return bolumListesi;
        }
        public List<HastanelerModel> HastaneGetir()
        {
            List<HastanelerModel> hastaneListesi = db.HastaneTablosu.ToList();
            return hastaneListesi;
        }
        public List<KullaniciModel> HastaGetir()
        {
            List<KullaniciModel> hastaListesi = db.KullaniciTablosu.AsNoTracking().ToList();
            return hastaListesi;
        }
        public List<KullaniciModel> HastaMailGetir(int id)
        {
            List<KullaniciModel> mailListesi = db.KullaniciTablosu.AsNoTracking().ToList();
            KullaniciModel hastaModel = mailListesi.Find(x => x.KullaniciId == id);
            if (hastaModel != null)
            {
                mailListesi.Remove(hastaModel);
            }
            return mailListesi;
        }
        public List<DoktorModel> mailgetir(int id)
        {
            List<DoktorModel> mailListesi = db.DoktorTablosu.AsNoTracking().ToList();
            DoktorModel doktorModel = mailListesi.Find(x=> x.DoktorId==id);
            if(doktorModel != null)
            {
                mailListesi.Remove(doktorModel);
            }


            return mailListesi;
        }
        public IActionResult Hasta()
        {
            var hastalar = db.KullaniciTablosu.ToList();
            
            return View(hastalar);
        }
        [HttpGet]
        public IActionResult HastaEdit(int id)
        {
            ViewBag.hasta = HastaGetir();
            var Model = db.KullaniciTablosu.Find(id);
            if (Model == null)
            {
                return NotFound();
            }
            ViewBag.id = id;
            return View(Model);
        }
        [HttpPost]
        public IActionResult HastaEdit(int id, KullaniciModel Hasta)
        {
            ViewBag.hasta = HastaGetir();
            Hasta.KullaniciId = id;


            if (ModelState.IsValid)
            {
                var hastalar = HastaMailGetir(id);
                if (hastalar.Any(p => p.KullaniciMail == Hasta.KullaniciMail))
                {
                    ViewBag.msg = "Bu mail kullanılıyor.";
                    return View(Hasta);
                }
                else
                {

                    db.Update(Hasta);
                    db.SaveChanges();
                    return RedirectToAction("Hasta");
                }
            }
            else
            {
                return View(Hasta);
            }
        }
        public IActionResult HastaDelete(int id)
        {
            var hasta = db.KullaniciTablosu.Find(id);
            if (hasta == null)
            {
                return NotFound(); // Öğe bulunamazsa 404 Not Found döndür
            }
            return View(hasta);
        }
        [HttpPost, ActionName("HastaDelete")]
        public IActionResult HastaDeleteComplete(int id)
        {
            var hasta = db.KullaniciTablosu.Find(id);
            if (hasta == null)
            {
                return NotFound(); // Öğe bulunamazsa 404 Not Found döndür
            }
            db.KullaniciTablosu.Remove(hasta);
            db.SaveChanges();

            return RedirectToAction("Hasta");
        }
        public IActionResult HastaEkle()
        {
            //ViewBag.BolumId = BolumGetir();
            return View();

        }
        [HttpPost]
        public IActionResult HastaEkle(KullaniciModel y)
        {
           // ViewBag.BolumId = BolumGetir();
            if (ModelState.IsValid)
            {
                if (db.KullaniciTablosu.Any(p => p.KullaniciTcNo == y.KullaniciTcNo))
                {
                    ViewBag.msg = "TcNo hatalı...";
                    return View(y);
                }
                else if (db.KullaniciTablosu.Any(p => p.KullaniciMail == y.KullaniciMail))
                {
                    ViewBag.msg = "Mail hatalı...";
                    return View(y);
                }
                db.Add(y);
                db.SaveChanges();
                return RedirectToAction("Hasta");
            }
            else
            {
                ViewBag.msg = "EKLENEMEDİ...";
                return View(y);
            }
        }
        public IActionResult Bolum()
        {
            var bolumler = db.BolumTablosu.ToList();
            return View(bolumler);
        }
        public IActionResult BolumEkle()
        {
            //ViewBag.BolumId = BolumGetir();
            return View();

        }
        [HttpPost]
        public IActionResult BolumEkle(BolumModel y)
        {
            // ViewBag.BolumId = BolumGetir();
            if (ModelState.IsValid)
            {
                db.Add(y);
                db.SaveChanges();
                return RedirectToAction("Bolum");
            }
            else
            {
                ViewBag.msg = "EKLENEMEDİ...";
                return View(y);
            }
        }
        public IActionResult BolumDelete(int id)
        {
            var bolum = db.BolumTablosu.Find(id);
            if (bolum == null)
            {
                return NotFound(); // Öğe bulunamazsa 404 Not Found döndür
            }
            return View(bolum);
        }
        [HttpPost, ActionName("BolumDelete")]
        public IActionResult BolumDeleteComplete(int id)
        {
            var bolum = db.BolumTablosu.Find(id);
            if (bolum == null)
            {
                return NotFound(); // Öğe bulunamazsa 404 Not Found döndür
            }
            db.BolumTablosu.Remove(bolum);
            db.SaveChanges();

            return RedirectToAction("Bolum");
        }
        [HttpGet]
        public IActionResult BolumEdit(int id)
        {
            ViewBag.bolum = BolumGetir();
            var Model = db.BolumTablosu.Find(id);
            if (Model == null)
            {
                return NotFound();
            }
            ViewBag.id = id;
            return View(Model);
        }
        [HttpPost]
        public IActionResult BolumEdit(int id, BolumModel Bolum)
        {
            ViewBag.bolum = HastaGetir();
            Bolum.BolumId = id;

            if (ModelState.IsValid)
            {
                db.Update(Bolum);
                db.SaveChanges();
                return RedirectToAction("Bolum");

            }
            else
            {
                ViewBag.msg = "Güncellenemedi.";
                return View(Bolum);
            }
        }






















    }

  
}
