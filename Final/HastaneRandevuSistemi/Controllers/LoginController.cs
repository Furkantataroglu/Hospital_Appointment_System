using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace HastaneRandevuSistemi.Controllers
{
    public class LoginController : Controller
    {
        MyContext db = new MyContext();
        public IActionResult Index()
        {


            return View();
        }
        public IActionResult CikisYap()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult GirisYap() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult GirisYap(string sifre, string mail)   //MAİL OLACAK
        {
            var birlesiktablo = db.DoktorTablosu.Select(doktor => new { Mail = doktor.DoktorMail, Sifre = doktor.DoktorSifre, State = doktor.state})
                .Union(db.KullaniciTablosu.Select(kullanici => new { Mail = kullanici.KullaniciMail, Sifre = kullanici.KullaniciSifre, State=kullanici.state }));
            
            var tabloIcerik = birlesiktablo.FirstOrDefault(p=> p.Mail == mail && p.Sifre == sifre  );
            var kontrol = birlesiktablo.Any(p => p.Mail == mail && p.Sifre == sifre);
            if (kontrol == true)
            {
                if (tabloIcerik != null)
                {


                    if (tabloIcerik.State == "Hasta")
                    {
                        var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, mail),
                    new Claim(ClaimTypes.Role, "Hasta")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                        var principal = new ClaimsPrincipal(identity);
                        var auth = new AuthenticationProperties();
                        var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, auth);

                        return RedirectToAction("RandevuAl", "Randevu");
                    }
                    else if (tabloIcerik.State == "Doktor")
                    {
                        var doktorid = db.DoktorTablosu.Where(p => p.DoktorMail == mail).Select(p => p.DoktorId).FirstOrDefault(); ///doktor id ataması
                        var identity = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Email, mail),
                      new Claim(ClaimTypes.Name ,doktorid.ToString()), ////////////////////
                      new Claim(ClaimTypes.Role, "Doktor")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                        var principal = new ClaimsPrincipal(identity);
                        var auth = new AuthenticationProperties();
                        var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, auth);

                        return RedirectToAction("Index", "Doktor", new { area = "Doktor" });
                    }
                    else if (tabloIcerik.State == "Admin")
                    {
                        var identity = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Email, mail),
                      new Claim(ClaimTypes.Role, "Admin")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                        var principal = new ClaimsPrincipal(identity);
                        var auth = new AuthenticationProperties();
                        var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, auth);

                        return RedirectToAction("Dashboard", "Admin", new { area = "Admin" });
                    }
                    else if (tabloIcerik.State == "Baskan")
                    {
                        var identity = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Email, mail),
                      new Claim(ClaimTypes.Role, "Baskan")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                        var principal = new ClaimsPrincipal(identity);
                        var auth = new AuthenticationProperties();
                        var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, auth);

                        return RedirectToAction("CalismaZamaniBelirle", "CalismaSaatleri", new { area = "Baskan" });
                    }

                }
            }
            else
            {
                ViewBag.msg = "Mail veya Şifre Hatalı";
            }
        
            return View();
        }

        public IActionResult kayitol()
        {
            return RedirectToAction("kullaniciEkle", "Kullanici");
        }


    }
}
