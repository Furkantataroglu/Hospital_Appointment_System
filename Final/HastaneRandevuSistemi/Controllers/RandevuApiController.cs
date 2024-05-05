using HastaneRandevuSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HastaneRandevuSistemi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandevuApiController : ControllerBase
    {
        MyContext db = new MyContext();

        // GET: api/<RandevuApiController>
        [HttpGet]
        public List<RandevuModel> Get()
        {
            return db.RandevuTablosu.ToList();
        }

        // GET api/<RandevuApiController>/5
        [HttpGet("{id}")]
        public RandevuModel Get(int id)
        {
            var randevu = db.RandevuTablosu.FirstOrDefault(x => x.Id == id);
            return randevu;
        }

        // POST api/<RandevuApiController>
        //[HttpPost]
        //public void Post([FromBody] string value)            ///KAYDETMEK İÇİN
        //{
        //}

        // PUT api/<RandevuApiController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)      ///GÜNCELLEME YAPMAK İÇİN
        //{
        //}

        // DELETE api/<RandevuApiController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)                  ///SİLME İŞLEMİ İÇİN
        {
            var randevu = db.RandevuTablosu.FirstOrDefault(p=>p.Id == id);
            if(randevu is null)
            {
                return NotFound();
            }
            else
            {
                db.Remove(randevu);
                db.SaveChanges();
                return Ok();
            }
        }
    }
}
