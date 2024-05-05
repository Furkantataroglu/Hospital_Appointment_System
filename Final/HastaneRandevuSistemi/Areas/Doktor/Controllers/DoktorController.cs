using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HastaneRandevuSistemi.Areas.Doktor.Controllers
{
    [Area("Doktor")]
    [Authorize(Roles = "Doktor")]
    public class DoktorController : Controller
    {
       // private List<RandevuModel> values;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CikisYap()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        [HttpGet]
        public async Task<IActionResult> randevulisteleme()
        {
            var client = new HttpClient();
            var responseMsg = await client.GetAsync("http://localhost:59823/api/RandevuApi");
            var jsonString = await responseMsg.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<RandevuModel>>(jsonString);           /////orderby olması gerekli

            var authList = User.Claims.ToList();
            var id = authList[1].Value;

            for(int i = values.Count(); i>0 ; i--)
            {
                if(values[i-1].DoktorNO != Int32.Parse(id))
                {
                    values.Remove(values[i-1]);
                }
            }
            return View(values);
        }

        public async Task<IActionResult> randevusilme(int id)
        {
            var client = new HttpClient();
            var responseMsg = await client.GetAsync("http://localhost:59823/api/RandevuApi");
            var jsonString = await responseMsg.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<RandevuModel>>(jsonString);
            var authList = User.Claims.ToList();
            var dkid = authList[1].Value;


            for(int i = values.Count(); i > 0; i--)
            {
                if (Int32.Parse(dkid) == values[i-1].DoktorNO && values[i-1].Id==id)
                {
                    var client2 = new HttpClient();
                    var responseMsg2 = await client.DeleteAsync("http://localhost:59823/api/RandevuApi/" + id);
                    if (responseMsg2.IsSuccessStatusCode)     ///// farklı doktor baska doktor silmemesi lazım......
                    {
                        return RedirectToAction("randevulisteleme");
                    }
                    return RedirectToAction("randevulisteleme");
                }
            }

            


            return RedirectToAction("randevulisteleme");


        }
    }
}
