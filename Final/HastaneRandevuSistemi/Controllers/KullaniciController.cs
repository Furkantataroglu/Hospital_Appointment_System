using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HastaneRandevuSistemi.Controllers
{
    public class KullaniciController : Controller
    {
        private MyContext db = new MyContext();
        
        public IActionResult Index()
        {
            return View();
        }
        
        
        public IActionResult kullaniciEkle()
        {
            return View();
            
        }

        [HttpPost]
        public IActionResult kullaniciEkle(KullaniciModel y)
        {
            
            var tcler = db.DoktorTablosu.Any(p => p.DoktorTcNo == y.KullaniciTcNo);
            
            if (ModelState.IsValid && tcler==false)
            {
                if (db.KullaniciTablosu.Any(p=>p.KullaniciTcNo==y.KullaniciTcNo))
                {
                    ViewBag.msg = "TcNo hatalı...";
                    return View(y);
                }
                else if(db.KullaniciTablosu.Any(p => p.KullaniciMail == y.KullaniciMail))
                {
                    ViewBag.msg = "Mail hatalı...";
                    return View(y);
                }
                db.Add(y);
                db.SaveChanges();
                return RedirectToAction("GirisYap","Login");
            }
            else
            {
                ViewBag.msg = "EKLENEMEDİ...";
                return View(y);
            }
        }
    }
}
