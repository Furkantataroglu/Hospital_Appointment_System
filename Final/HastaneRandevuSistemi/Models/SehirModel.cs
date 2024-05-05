using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneRandevuSistemi.Models
{
    public class SehirModel
    {
        [Key]
        public int SehirId { get; set; }
        public string SehirAdi { get; set; }
        
    

    }
}
