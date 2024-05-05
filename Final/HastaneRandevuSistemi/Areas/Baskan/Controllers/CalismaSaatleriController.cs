using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HastaneRandevuSistemi.Areas.Baskan.Controllers
{
    [Authorize(Roles = "Baskan")]
    [Area("Baskan")]
    public class CalismaSaatleriController : Controller
    {
     

        private MyContext db = new MyContext();

        public IActionResult CikisYap()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CalismaZamaniBelirle()
        {
            ViewBag.Doktorlar = DoktorGetir();
            return View();
        }
        [HttpPost]
        public IActionResult CalismaZamaniBelirle(int selectedDoktorId, string DCSMBaslangicSaati, string DCSMBitisSaati, string DCSMcalismaTarihi)
        {
            ViewBag.Doktorlar = DoktorGetir(); ///// buradaki tablolar oto dolması gererkli
            if (ModelState.IsValid)
            {
                int saat = 0;
                int saat1 = 0;
                string[] baslangicsaatParcalari = DCSMBaslangicSaati.Split(':');
                string[] bitissaatParcalari = DCSMBitisSaati.Split(':');
                int.TryParse(baslangicsaatParcalari[0], out saat);
                int.TryParse(bitissaatParcalari[0], out saat1);
                if (saat1 > saat)
                {
                    if (db.doktorCalismaSaatlariModeliTablosu.Any(p => p.DCSMcalismaTarihi == DCSMcalismaTarihi && p.DoktorId == selectedDoktorId))
                    {
                        var sonuc = db.doktorCalismaSaatlariModeliTablosu
                        .Where(a => a.DCSMcalismaTarihi == DCSMcalismaTarihi && a.DoktorId == selectedDoktorId)
                        .Select(a => a.DCSMId)
                        .FirstOrDefault();
                        var model = new DoktorCalismaSaatlariModeli
                        {
                            DCSMId = sonuc,
                            DoktorId = selectedDoktorId,
                            DCSMBaslangicSaati = DCSMBaslangicSaati,
                            DCSMBitisSaati = DCSMBitisSaati,
                            DCSMcalismaTarihi = DCSMcalismaTarihi
                        };
                        db.Update(model);
                    }
                    else
                    {
                        var model = new DoktorCalismaSaatlariModeli
                        {
                            DoktorId = selectedDoktorId,
                            DCSMBaslangicSaati = DCSMBaslangicSaati,
                            DCSMBitisSaati = DCSMBitisSaati,
                            DCSMcalismaTarihi = DCSMcalismaTarihi
                        };
                        db.Add(model);
                    }
                   
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.msg = "Başlangıç saati bitiş saatinden eşit ya da yüksek olamaz";
                }
                


            }
            return View();
        }
        
        


        public List<DoktorModel> DoktorGetir()
        {
            List<DoktorModel> doktorListesi = db.DoktorTablosu.ToList();
            return doktorListesi;

        }
    }
}
