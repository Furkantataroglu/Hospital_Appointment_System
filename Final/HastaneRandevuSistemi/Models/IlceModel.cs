using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneRandevuSistemi.Models
{
    public class IlceModel
    {
        [Key]
        public int ilceid { get; set; }

        [ForeignKey("SehirId")]
        public int Sehirid { get; set; }
        public SehirModel sehir { get; set; }
        public string IlceAdi { get; set; }


        

    }
}
