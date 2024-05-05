using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneRandevuSistemi.Models
{
    public class RandevuModel
    {
        [Key]
        public int Id { get; set; }
        public string HastaTcNo { get; set; }
        public string HastaAdi { get; set; }
        public string HastaSoyadi { get; set; }
        public int DoktorNO { get; set; }
        public string DoktorAdi { get; set; }
        public string DoktorSoyadi { get; set; }
        public string Gun { get; set; }
        public string Saat { get; set; }


    }
}
