using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace HastaneRandevuSistemi.Models
{
    public class BolumModel
    {
        [Key]
        public int BolumId { get; set; }
        public string BolumAdi { get; set; }
    }
}
