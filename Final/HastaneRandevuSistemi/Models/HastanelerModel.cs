using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneRandevuSistemi.Models
{
    public class HastanelerModel
    {
        [Key]
        public int HastaneId { get; set; }
       
        [ForeignKey("ilceid")]
        public int ilceid { get; set; }
        public IlceModel ilce { get; set; }

        public string HastaneAdi { get; set; }

        public ICollection<DoktorModel> doktor { get; set; }

    }
}
