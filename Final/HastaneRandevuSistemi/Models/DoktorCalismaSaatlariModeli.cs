using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneRandevuSistemi.Models
{
    public class DoktorCalismaSaatlariModeli
    {
        [Key]
        public int DCSMId { get; set; }
        [ForeignKey("DoktorId")]        
        public int DoktorId { get; set; }
        [Required]
        public string DCSMBaslangicSaati { get; set; }
        [Required]
        public string DCSMBitisSaati { get; set; }
        [Required]
        public string DCSMcalismaTarihi { get; set; }



    }
}
